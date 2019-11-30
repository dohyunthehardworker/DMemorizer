using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace Main
{
    public partial class FormMain : Form
    {
        string userName = "";
        string userEmail = "";
        string userAuth = "USER";
        int last_test_level = 0; //마지막 공부한 위치
        int test_idx = 0;
        int test_group_idx = 0;
        int lang_idx = 0;
        bool comboLoaded = false;
        List<ClassWord> classWordlist;
        string connectionString = @"Data Source=leedohyun.asuscomm.com,1433;Initial Catalog=DMemorizer;User ID=sa;Password=P@ssw0rd;";
        Random rnd = new Random();
        bool blinkFlag = false; //깜빡이용 
        string sheetName = "";
        public FormMain()
        {
            InitializeComponent();
            //this.WindowState = FormWindowState.Maximized;

            //2초
            System.Timers.Timer autoTimer = new System.Timers.Timer(4000);
            autoTimer.AutoReset = true;
            autoTimer.Elapsed += new System.Timers.ElapsedEventHandler(AutoTimer);
            autoTimer.Start();

            //1초
            System.Timers.Timer autoTimerSec = new System.Timers.Timer(500);
            autoTimerSec.AutoReset = true;
            autoTimerSec.Elapsed += new System.Timers.ElapsedEventHandler(AutoTimerSec);
            autoTimerSec.Start();

        }

        /***********************************************************************************
        공통함수           
        ************************************************************************************/

        /// <summary>
        /// SHA512 단방향 암호화 함수
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static byte[] GetCrypt(string text)
        {
            SHA512 sHA512 = SHA512.Create();
            byte[] result = sHA512.ComputeHash(Encoding.UTF8.GetBytes(text));
            return result;
        }
        /// <summary>
        /// 회원가입 함수
        /// </summary>
        private void SignUp()
        {
            FormSignUp formSignUp = new FormSignUp();
            formSignUp.StartPosition = FormStartPosition.CenterParent;
            formSignUp.ShowDialog(this);
            if (formSignUp.DialogResult == DialogResult.OK)
            {
                int result = SignUpProcess(formSignUp.textBoxID.Text, formSignUp.textBoxName.Text, formSignUp.textBoxPassword.Text, formSignUp.textBoxPasswordCheck.Text);

                if (result == 1)
                {//회원가입 성공
                    formSignUp.Close();
                    buttonLogOut.Show();
                    menuStripMain.Show();
                    flowLayoutPanelCombo.Show();
                    buttonLogin.Hide();
                    buttonSignUp.Hide();
                    labelName.Text = userName;
                    setComboBoxLanguage();
                    flowLayoutPanelCombo.Visible = true;
                    listViewWordList.Show();
                    richTextBoxWord.Show();
                    comboLoaded = true;
                }
                else
                {
                    formSignUp.Close();
                    SignUp();
                }
            }
            else if (formSignUp.DialogResult == DialogResult.Yes)
            {
                formSignUp.Close();
                LogIn();
            }
            else
            {
                formSignUp.Close();
            }
        }

        /// <summary>
        /// 로그인 함수
        /// </summary>
        private void LogIn()
        {

            FormLogIn formLogIn = new FormLogIn();
            formLogIn.StartPosition = FormStartPosition.CenterParent;
            formLogIn.ShowDialog(this);
            if (formLogIn.DialogResult == DialogResult.OK)
            {
                int result = LogInProcess(formLogIn.textBoxID.Text, formLogIn.textBoxPassword.Text);

                if (result == 1)
                {//로그인 성공
                    formLogIn.Close();
                    buttonLogOut.Show();
                    menuStripMain.Show();
                    flowLayoutPanelCombo.Show();
                    buttonLogin.Hide();
                    buttonSignUp.Hide();
                    labelName.Text = userName;
                    setComboBoxLanguage();
                    flowLayoutPanelCombo.Visible = true;
                    comboLoaded = true;
                    listViewWordList.Show();
                    richTextBoxWord.Show();
                }
                else
                {
                    formLogIn.Close();
                    LogIn();
                }
            }
            else if (formLogIn.DialogResult == DialogResult.Yes)
            {
                formLogIn.Close();
                SignUp();
            }
            else
            {
                formLogIn.Close();
            }
        }
        private void LogOut()
        {
            //데이터 클리어
            userAuth = "USER";
            userEmail = "";
            userName = "";
            labelName.Text = "";

            last_test_level = 0; //마지막 공부한 위치
            test_idx = 0;
            test_group_idx = 0;
            lang_idx = 0;

            buttonAdmin.Hide();
            buttonLogOut.Hide();
            menuStripMain.Hide();
            buttonLogin.Show();
            buttonSignUp.Show();
            flowLayoutPanelCombo.Hide();
            toolStripStatusLabelMain.Text = "로그아웃 성공";
            flowLayoutPanelCombo.Visible = false;
            comboLoaded = false;
            listViewWordList.Hide();
            flowLayoutPanelDay.Controls.Clear();

            richTextBoxWord.Text = "";
            richTextBoxWord.Hide();
            checkBoxAuto.Hide();
            checkBoxAuto.Checked = false;
            checkBoxBlink.Hide();
            checkBoxBlink.Checked = false;
            checkBoxRandom.Hide();
            checkBoxRandom.Checked = false;
            checkBoxRepeat.Hide();
            checkBoxRepeat.Checked = false;
            LogIn();
        }

        /// <summary>
        /// 로그인 프로세스 함수
        /// </summary>
        private int LogInProcess(string id, string password)
        {
            if (id == "" || password == "")
            {

                MessageBox.Show("아이디와 패스워드를 입력하세요.");
                return 0;
            }
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand("SELECT * FROM user_mst WHERE user_email=@user_email AND user_password=@user_password", connection);
                command.Parameters.AddWithValue("@user_email", id);
                command.Parameters.AddWithValue("@user_password", GetCrypt(password));
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                connection.Close();
                int count = dataSet.Tables[0].Rows.Count;

                if (count == 1)
                {
                    userAuth = dataSet.Tables[0].Rows[0]["user_auth"].ToString();
                    userEmail = dataSet.Tables[0].Rows[0]["user_email"].ToString();
                    userName = dataSet.Tables[0].Rows[0]["user_name"].ToString();

                    if (dataSet.Tables[0].Rows[0]["last_level"] != DBNull.Value)
                    {
                        last_test_level = (int)dataSet.Tables[0].Rows[0]["last_level"];
                    }

                    if (dataSet.Tables[0].Rows[0]["user_auth"].ToString().Equals("0"))
                    {//관리자일 경우 이름 뒤에 '(관리자)'를 붙인다.
                        buttonAdmin.Show();
                        userAuth = "ADMIN";
                        userName += "(관리자)";
                    }

                    toolStripStatusLabelMain.Text = "로그인 성공";
                    return count;
                }
                else if (count > 1)
                {//중복회원이 있는 경우
                    MessageBox.Show("로그인 실패");
                    return 0;
                }
                else
                {
                    MessageBox.Show("로그인 실패");
                    return 0;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return 0;
            }
        }

        /// <summary>
        /// 회원가입 프로세스 함수
        /// </summary>
        private int SignUpProcess(string id, string name, string password, string passwordCheck)
        {

            if (id == "" || password == "" || passwordCheck == "")
            {
                MessageBox.Show("아이디와 패스워드, 패스워드 확인값을 입력하세요.");
                return 0;
            }
            if (name == "")
            {
                MessageBox.Show("이름은 필수값입니다.");
                return 0;
            }
            if (!password.Equals(passwordCheck))
            {
                MessageBox.Show("패스워드 확인값이 패스워드와 동일하지 않습니다.");
                return 0;
            }
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(
                    "INSERT INTO user_mst (user_email, user_password, user_name, user_use_flag, user_email_checked, insert_user, update_user) " +
                    "VALUES (@user_email, @user_password, @user_name, 1, 1, 1, 1)", connection);
                command.Parameters.AddWithValue("@user_email", id);
                command.Parameters.AddWithValue("@user_name", name);
                command.Parameters.AddWithValue("@user_password", GetCrypt(password));
                connection.Open();
                int count = command.ExecuteNonQuery();
                connection.Close();

                if (count == 1)
                {
                    toolStripStatusLabelMain.Text = "회원가입 성공";

                    userEmail = id;
                    userName = name;
                    userAuth = "USER";
                    return 1;
                }
                else
                {
                    MessageBox.Show("회원가입 실패");
                    return 0;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return 0;
            }
        }
        /// <summary>
        /// 콤포박스에 학습 언어 세팅 함수
        /// </summary>
        private void setComboBoxLanguage()
        {
            //이전에 학습한 이력이 있으면
            if (last_test_level != 0)
            {
                //시험기본키, 시험그룹기본키, 언어기본키 가져오기
                try
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    string query = "select"
                    + "(select test_idx from test_level_mst where test_level_idx = @test_level_idx) as test_idx"
                    + ",(select test_group_idx from test_mst where test_idx = (select test_idx from test_level_mst where test_level_idx = @test_level_idx) ) as test_group_idx"
                    + ",(select lang_idx from test_group_mst where test_group_idx = (select test_group_idx from test_mst where test_idx = (select test_idx from test_level_mst where test_level_idx = @test_level_idx) )) as lang_idx"
                    ;
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@test_level_idx", last_test_level);
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    connection.Close();

                    test_idx = (int)dataSet.Tables[0].Rows[0]["test_idx"];
                    test_group_idx = (int)dataSet.Tables[0].Rows[0]["test_group_idx"];
                    lang_idx = (int)dataSet.Tables[0].Rows[0]["lang_idx"];

                    //언어 콤보박스 설치
                    try
                    {
                        connection = new SqlConnection(connectionString);
                        command = new SqlCommand("SELECT * FROM lang_mst", connection);
                        connection.Open();
                        dataAdapter = new SqlDataAdapter(command);
                        dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
                        connection.Close();
                        int count = dataSet.Tables[0].Rows.Count;
                        comboBoxLanguage.DataSource = dataSet.Tables[0];
                        comboBoxLanguage.DisplayMember = "lang_name";
                        comboBoxLanguage.ValueMember = "lang_idx";
                        comboBoxLanguage.SelectedValue = lang_idx;
                        comboBoxLanguage.Show();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }

                    //시험 그룹 콤보박스 설치
                    try
                    {
                        connection = new SqlConnection(connectionString);
                        command = new SqlCommand("SELECT * FROM test_group_mst", connection);
                        connection.Open();
                        dataAdapter = new SqlDataAdapter(command);
                        dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
                        connection.Close();
                        int count = dataSet.Tables[0].Rows.Count;
                        comboBoxTestGroup.DataSource = dataSet.Tables[0];
                        comboBoxTestGroup.DisplayMember = "test_group_name";
                        comboBoxTestGroup.ValueMember = "test_group_idx";
                        comboBoxTestGroup.SelectedValue = test_group_idx;
                        comboBoxTestGroup.Show();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }

                    //시험 콤보박스 설치
                    try
                    {
                        connection = new SqlConnection(connectionString);
                        command = new SqlCommand("SELECT * FROM test_mst", connection);
                        connection.Open();
                        dataAdapter = new SqlDataAdapter(command);
                        dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
                        connection.Close();
                        int count = dataSet.Tables[0].Rows.Count;
                        comboBoxTest.DataSource = dataSet.Tables[0];
                        comboBoxTest.DisplayMember = "test_name";
                        comboBoxTest.ValueMember = "test_idx";
                        comboBoxTest.SelectedValue = test_idx;
                        comboBoxTest.Show();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }

                    //시험 레벨 콤보박스 설치
                    try
                    {
                        connection = new SqlConnection(connectionString);
                        command = new SqlCommand("SELECT * FROM test_level_mst", connection);
                        connection.Open();
                        dataAdapter = new SqlDataAdapter(command);
                        dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
                        connection.Close();
                        int count = dataSet.Tables[0].Rows.Count;
                        comboBoxTestLevel.DataSource = dataSet.Tables[0];
                        comboBoxTestLevel.DisplayMember = "test_level_name";
                        comboBoxTestLevel.ValueMember = "test_level_idx";
                        comboBoxTestLevel.SelectedValue = last_test_level;
                        comboBoxTestLevel.Show();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }


                    try
                    {
                        //플로우레이아웃패널내의 컨트롤 전체 삭제
                        flowLayoutPanelDay.Controls.Clear();

                        //MessageBox.Show(comboBoxTestLevel.SelectedValue.ToString());

                        //일자별 데이터 전체 호출
                        connection = new SqlConnection(connectionString);
                        command = new SqlCommand("SELECT DISTINCT word_day_info FROM word_mst WHERE test_level_idx = @test_level_idx ORDER BY word_day_info", connection);
                        command.Parameters.AddWithValue("@test_level_idx", Int32.Parse(comboBoxTestLevel.SelectedValue.ToString()));
                        connection.Open();
                        dataAdapter = new SqlDataAdapter(command);
                        dataSet = new DataSet();
                        dataAdapter.Fill(dataSet);
                        connection.Close();
                        int count = dataSet.Tables[0].Rows.Count;
                        //MessageBox.Show(count.ToString());
                        for (int i = 0; i < count; i++)
                        {
                            Button button = new Button();
                            button.Tag = i;
                            button.Text = "Day " + dataSet.Tables[0].Rows[i]["word_day_info"].ToString();
                            button.Click += DayButton_Click;
                            flowLayoutPanelDay.Controls.Add(button);
                        }
                        flowLayoutPanelDay.Show();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }

            }
            else
            {
                comboBoxTestGroup.Hide();
                comboBoxTest.Hide();
                comboBoxTestLevel.Hide();

                try
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand("SELECT * FROM lang_mst", connection);
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    connection.Close();
                    int count = dataSet.Tables[0].Rows.Count;
                    comboBoxLanguage.DataSource = dataSet.Tables[0];
                    comboBoxLanguage.DisplayMember = "lang_name";
                    comboBoxLanguage.ValueMember = "lang_idx";
                    comboBoxLanguage.Show();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }

            //throw new NotImplementedException();
            //언어

            //회원의 언어 설정이 있으면 세팅하기

        }

        /// <summary>
        /// 자동 학습용 타이머
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoTimer(object sender, System.Timers.ElapsedEventArgs e)
        {
            
            //자동으로 체크가 되어 있으면 
            if (checkBoxAuto.Checked)
            {
                if (listViewWordList.SelectedIndices.Count <= 0)
                {
                    listViewWordList.Items[0].Selected = true;
                }

                int intselectedindex = listViewWordList.SelectedIndices[0];
                if (checkBoxRepeat.Checked)
                {//반복 체크가 되어 있는 경우

                    if (checkBoxRandom.Checked)
                    {
                        listViewWordList.Items[rnd.Next(0, listViewWordList.Items.Count)].Selected = true;
                    }
                    else
                    {//랜덤 체크가 되어 있지 않은 경우
                        if (listViewWordList.SelectedIndices[0] == listViewWordList.Items.Count - 1)
                        {
                            listViewWordList.Items[0].Selected = true;
                        }
                        else
                        {
                            listViewWordList.Items[listViewWordList.SelectedIndices[0] + 1].Selected = true;
                        }
                    }
                }
                else
                {//반복체크가 되어 있지 않은 경우
                    if (listViewWordList.SelectedIndices[0] != listViewWordList.Items.Count - 1)
                    {
                        listViewWordList.Items[listViewWordList.SelectedIndices[0] + 1].Selected = true;
                    }
                }
            }
        }
        /// <summary>
        /// 깜빡이 타이머
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoTimerSec(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (checkBoxBlink.Checked)
            {
                if (blinkFlag)
                {
                    richTextBoxWord.Hide();
                    blinkFlag = false;
                }
                else
                {
                    richTextBoxWord.Show();
                    blinkFlag = true;
                }
            }

        }

        /***********************************************************************************
        이벤트함수           
        ************************************************************************************/

        /// <summary>
        /// 화면 로드 완료 후 동작 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Shown(object sender, EventArgs e)
        {
            LogIn();

        }
        /// <summary>
        /// 회원 가입 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSignUp_Click(object sender, EventArgs e)
        {
            SignUp();
        }
        /// <summary>
        /// 로그인 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            LogIn();
        }
        /// <summary>
        /// 로그 아웃 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            LogOut();
        }
        /// <summary>
        /// 회원관리 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdmin_Click(object sender, EventArgs e)
        {
            FormAdmin formAdmin = new FormAdmin();
            formAdmin.StartPosition = FormStartPosition.CenterParent;
            formAdmin.ShowDialog(this);
        }
        /// <summary>
        /// 내보내기 메뉴 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 내보내기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //내보낼 데이터 있는지 체크

            if (listViewWordList.SelectedIndices.Count <= 0)
            {
                MessageBox.Show("저장할 데이터가 존재하지 않습니다.", "경고", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel 통합문서|*.xlsx";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "엑셀 파일 저장";
                saveFileDialog.FileName = sheetName;   //기본 이름 설정
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    String filepath = "";
                    if (!Path.HasExtension(saveFileDialog.FileName) || Path.GetExtension(saveFileDialog.FileName) != ".xlsx")
                    {
                        filepath = saveFileDialog.FileName + ".xlsx";
                    }
                    else
                    {
                        filepath = saveFileDialog.FileName;
                    }

                    if (filepath != null)
                    {
                        Excel.Application application = new Excel.Application();
                        Excel.Workbook workbook = application.Workbooks.Add();  //새 파일 추가
                        Excel.Worksheet worksheet = workbook.Worksheets.Item["Sheet1"]; //새로 생성된 시트 선택

                        //단어 입력 로직 추가

                        worksheet.Name = sheetName;                             // 시트 이름 설정
                        Excel.Range range;
                        //Excel.Worksheet worksheet = workbook.Worksheets.Add();  //새 시트 추가
                        for (int i = 0; i < classWordlist.Count; i++)
                        {
                            ClassWord classWord = classWordlist[i];

                            if (i == 0)
                            {//첫번째 열에 칼럼명 추가
                                range = worksheet.Cells[i + 1, 1];               //입력 범위 지정
                                range.Value = "순번";                              //입력 값 설정
                                range = worksheet.Cells[i + 1, 2];               //입력 범위 지정
                                range.Value = "단어";                              //입력 값 설정                                
                                range = worksheet.Cells[i + 1, 3];               //입력 범위 지정
                                range.Value = "발음";                              //입력 값 설정
                                range = worksheet.Cells[i + 1, 4];               //입력 범위 지정
                                range.Value = "품사";                              //입력 값 설정
                                range = worksheet.Cells[i + 1, 5];               //입력 범위 지정
                                range.Value = "뜻";                              //입력 값 설정
                                range = worksheet.Cells[i + 1, 6];               //입력 범위 지정
                                range.Value = "품사2";                              //입력 값 설정
                                range = worksheet.Cells[i + 1, 7];               //입력 범위 지정
                                range.Value = "뜻2";                              //입력 값 설정
                                range = worksheet.Cells[i + 1, 8];               //입력 범위 지정
                                range.Value = "품사3";                              //입력 값 설정
                                range = worksheet.Cells[i + 1, 9];               //입력 범위 지정
                                range.Value = "뜻3";                              //입력 값 설정
                                range = worksheet.Cells[i + 1, 10];               //입력 범위 지정
                                range.Value = "품사4";                              //입력 값 설정
                                range = worksheet.Cells[i + 1, 11];               //입력 범위 지정
                                range.Value = "뜻4";                              //입력 값 설정
                                range = worksheet.Cells[i + 1, 12];               //입력 범위 지정
                                range.Value = "품사5";                              //입력 값 설정
                                range = worksheet.Cells[i + 1, 13];               //입력 범위 지정
                                range.Value = "뜻5";                              //입력 값 설정
                                range = worksheet.Cells[i + 1, 14];               //입력 범위 지정
                                range.Value = "품사6";                              //입력 값 설정
                                range = worksheet.Cells[i + 1, 15];               //입력 범위 지정
                                range.Value = "뜻7";                              //입력 값 설정
                                range = worksheet.Cells[i + 1, 16];               //입력 범위 지정
                                range.Value = "뜻7";                              //입력 값 설정
                            }

                            range = worksheet.Cells[i + 2, 1];               //입력 범위 지정
                            range.Value = i+1;                              //입력 값 설정
                            range = worksheet.Cells[i + 2, 2];               //입력 범위 지정
                            range.Value = classWord.Word;

                            for (int j = 0; j < classWord.WordDetailList.Count; j++)
                            {
                                ClassWordDetail classWordDetail = classWord.WordDetailList[j];
                                if (j == 0)
                                {
                                    range = worksheet.Cells[i + 2, 3];               //입력 범위 지정
                                    range.Value = classWordDetail.WordPronounce;
                                }
                                range = worksheet.Cells[i + 2, 4 + (j * 2)];               //입력 범위 지정
                                range.Value = classWordDetail.WordParts;
                                range = worksheet.Cells[i + 2, 5 + +(j * 2)];               //입력 범위 지정
                                range.Value = classWordDetail.WordMeaning;
                            }

                        }
                        workbook.SaveAs(filepath);
                        workbook.Close();
                        MessageBox.Show("저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);//내용, 제목, 확인 버튼, 가운데 이모티콘
                    }
                    toolStripStatusLabelMain.Text = "오늘의 단어 엑셀 저장하기 완료";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 : " + ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        /// <summary>
        /// 불러오기 메뉴 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 불러오기ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Excel 통합문서|*.xlsx";
            openFileDialog.Title = "엑셀 파일 열기";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                List<ClassWord> words = new List<ClassWord>();  //단어들을 담을 리스트 생성

                string fileName = openFileDialog.FileName;
                Excel.Application application = new Excel.Application();
                Excel.Workbook workbook = application.Workbooks.Open(fileName);  //파일 열기
                //MessageBox.Show("시트의 개수 : " + workbook.Worksheets.Count);
                toolStripStatusLabelMain.Text = "업로드 시작";
                toolStripProgressBar.Visible = true;
                for (int i = 1; i < workbook.Worksheets.Count + 1; i++) //시트 순번은 1-Based Numbering 임
                {//시트의 개수만큼 순회


                    Excel.Worksheet worksheet = workbook.Worksheets.Item[i]; //워크시트 가져오기
                    //Console.WriteLine("시트시작");
                    //MessageBox.Show(i + " 번째 시트의 사용한 행 개수 :  " + worksheet.UsedRange.Rows.Count); //158
                    //MessageBox.Show(i + " 번째 시트의 사용한 열 개수 :  " + worksheet.UsedRange.Columns.Count); //22
                    int lang_idx = 0;           //언어기본키
                    string lang_name = "";           //언어기본이름
                    int test_group_idx = 0;     //시험그룹기본키
                    string test_group_name = "";     //시험그룹기본키이름
                    int test_idx = 0;           //시험기본키
                    string test_name = "";           //시험기본키이름
                    int test_level_idx = 0;     //시험등급기본키
                    string test_level_name = "";     //시험등급기본키이름

                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    int rowLength = worksheet.UsedRange.Rows.Count + 1;
                    int percent = 0;
                    for (int j = 3; j < worksheet.UsedRange.Rows.Count + 1; j++)
                    {
                        percent = (int)((j / (rowLength - 3)) * 100);
                        //Console.WriteLine(percent);
                        toolStripStatusLabelMain.Text = "업로드 중 : " + percent + " %";
                        toolStripProgressBar.Value = percent;

                        Excel.Range range = worksheet.Cells[j, 1];
                        int result = 0;
                        if (j == 3)
                        {//세번째 행의 데이터로 등급명 까지 확인하고 입력한다.

                            //1. 언어기본키값 가져오기

                            //언어명이 없으면 작업하지 않는다.
                            if (null != range.Value && !range.Value.Equals(""))
                            {
                                lang_name = range.Value;
                                //MessageBox.Show(lang_name);
                                //select
                                string query =
                                    "SELECT" +
                                    "   lang_idx " +
                                    "FROM" +
                                    "   lang_mst " +
                                    "WHERE" +
                                    "   lang_name = @lang_name ";

                                SqlCommand command = new SqlCommand(query, connection);
                                command.Parameters.AddWithValue("@lang_name", lang_name);
                                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                                DataSet dataSet = new DataSet();
                                dataAdapter.Fill(dataSet);
                                int count = dataSet.Tables[0].Rows.Count;
                                //MessageBox.Show(count.ToString());

                                //해당 언어가 없는 경우 입력한다.
                                if (count == 0)
                                {
                                    query =
                                        "INSERT INTO " +
                                        "   lang_mst " +
                                        "   (   lang_name" +
                                        "   ,   insert_user" +
                                        "   ,   update_user) " +
                                        "VALUES" +
                                        "   (" +
                                        "       @lang_name" +
                                        "   ,   1" +
                                        "   ,   1" +
                                        "   )";
                                    command = new SqlCommand(query, connection);
                                    command.Parameters.AddWithValue("@lang_name", lang_name);
                                    result = command.ExecuteNonQuery();


                                    if (result == 1)
                                    {//정상 입력되었다면 인덱스 값을 가져온다.
                                        query =
                                    "SELECT" +
                                    "   lang_idx " +
                                    "FROM" +
                                    "   lang_mst " +
                                    "WHERE" +
                                    "   lang_name = @lang_name ";

                                        command = new SqlCommand(query, connection);
                                        command.Parameters.AddWithValue("@lang_name", lang_name);
                                        dataAdapter = new SqlDataAdapter(command);
                                        dataSet = new DataSet();
                                        dataAdapter.Fill(dataSet);
                                        count = dataSet.Tables[0].Rows.Count;
                                        lang_idx = (int)dataSet.Tables[0].Rows[0]["lang_idx"];
                                    }

                                }
                                else
                                {//해당 언어가 있는 경우 값을 세팅한다.
                                    lang_idx = (int)dataSet.Tables[0].Rows[0]["lang_idx"];
                                }

                                //2. 시험그룹기본키 가져오기
                                range = worksheet.Cells[j, 2];
                                if (null != range.Value && !range.Value.Equals(""))
                                {
                                    test_group_name = range.Value;
                                    //MessageBox.Show(lang_name);
                                    //select
                                    query =
                                        "SELECT" +
                                        "   test_group_idx " +
                                        "FROM" +
                                        "   test_group_mst " +
                                        "WHERE" +
                                        "   test_group_name = @test_group_name ";

                                    command = new SqlCommand(query, connection);
                                    command.Parameters.AddWithValue("@test_group_name", test_group_name);
                                    dataAdapter = new SqlDataAdapter(command);
                                    dataSet = new DataSet();
                                    dataAdapter.Fill(dataSet);
                                    count = dataSet.Tables[0].Rows.Count;
                                    //MessageBox.Show(count.ToString());

                                    if (count == 0)
                                    {
                                        query =
                                            "INSERT INTO " +
                                            "   test_group_mst " +
                                            "   (   test_group_name" +
                                            "   ,   lang_idx" +
                                            "   ,   insert_user" +
                                            "   ,   update_user) " +
                                            "VALUES" +
                                            "   (" +
                                            "       @test_group_name" +
                                            "   ,   @lang_idx" +
                                            "   ,   1" +
                                            "   ,   1" +
                                            "   )";
                                        command = new SqlCommand(query, connection);
                                        command.Parameters.AddWithValue("@test_group_name", test_group_name);
                                        command.Parameters.AddWithValue("@lang_idx", lang_idx);
                                        result = command.ExecuteNonQuery();

                                        if (result == 1)
                                        {//정상 입력되었다면 인덱스 값을 가져온다.
                                            query =
                                        "SELECT" +
                                        "   test_group_idx " +
                                        "FROM" +
                                        "   test_group_mst " +
                                        "WHERE" +
                                        "   test_group_name = @test_group_name ";

                                            command = new SqlCommand(query, connection);
                                            command.Parameters.AddWithValue("@test_group_name", test_group_name);
                                            dataAdapter = new SqlDataAdapter(command);
                                            dataSet = new DataSet();
                                            dataAdapter.Fill(dataSet);
                                            count = dataSet.Tables[0].Rows.Count;
                                            test_group_idx = (int)dataSet.Tables[0].Rows[0]["test_group_idx"];
                                        }

                                    }
                                    else
                                    {
                                        test_group_idx = (int)dataSet.Tables[0].Rows[0]["test_group_idx"];
                                    }

                                    //3. 시험기본키 가져오기

                                    range = worksheet.Cells[j, 3];
                                    if (null != range.Value && !range.Value.Equals(""))
                                    {
                                        test_name = range.Value;
                                        //MessageBox.Show(lang_name);
                                        //select
                                        query =
                                            "SELECT" +
                                            "   test_idx " +
                                            "FROM" +
                                            "   test_mst " +
                                            "WHERE" +
                                            "   test_name = @test_name ";

                                        command = new SqlCommand(query, connection);
                                        command.Parameters.AddWithValue("@test_name", test_name);
                                        dataAdapter = new SqlDataAdapter(command);
                                        dataSet = new DataSet();
                                        dataAdapter.Fill(dataSet);
                                        count = dataSet.Tables[0].Rows.Count;
                                        //MessageBox.Show(count.ToString());

                                        if (count == 0)
                                        {
                                            query =
                                             "INSERT INTO " +
                                             "   test_mst " +
                                             "   (   test_name" +
                                             "   ,   test_group_idx" +
                                             "   ,   insert_user" +
                                             "   ,   update_user) " +
                                             "VALUES" +
                                             "   (" +
                                             "       @test_name" +
                                             "   ,   @test_group_idx" +
                                             "   ,   1" +
                                             "   ,   1" +
                                             "   )";
                                            command = new SqlCommand(query, connection);
                                            command.Parameters.AddWithValue("@test_name", test_name);
                                            command.Parameters.AddWithValue("@test_group_idx", test_group_idx);
                                            result = command.ExecuteNonQuery();


                                            if (result == 1)
                                            {//정상 입력되었다면 인덱스 값을 가져온다.
                                                query =
                                            "SELECT" +
                                            "   test_idx " +
                                            "FROM" +
                                            "   test_mst " +
                                            "WHERE" +
                                            "   test_name = @test_name ";

                                                command = new SqlCommand(query, connection);
                                                command.Parameters.AddWithValue("@test_name", test_name);
                                                dataAdapter = new SqlDataAdapter(command);
                                                dataSet = new DataSet();
                                                dataAdapter.Fill(dataSet);
                                                count = dataSet.Tables[0].Rows.Count;
                                                test_idx = (int)dataSet.Tables[0].Rows[0]["test_idx"];
                                            }

                                        }
                                        else
                                        {//해당 언어가 있는 경우 값을 세팅한다.
                                            test_idx = (int)dataSet.Tables[0].Rows[0]["test_idx"];
                                        }

                                        //4. 시험등급기본키 가져오기

                                        range = worksheet.Cells[j, 4];
                                        if (null != range.Value && !range.Value.Equals(""))
                                        {
                                            test_level_name = ((int)range.Value).ToString();
                                            //MessageBox.Show(lang_name);
                                            //select
                                            query =
                                                "SELECT" +
                                                "   test_level_idx " +
                                                "FROM" +
                                                "   test_level_mst " +
                                                "WHERE" +
                                                "   test_level_name = @test_level_name "
                                                + " AND "
                                                + " test_idx = @test_idx "
                                                ;

                                            command = new SqlCommand(query, connection);
                                            command.Parameters.AddWithValue("@test_level_name", test_level_name);
                                            command.Parameters.AddWithValue("@test_idx", test_idx);

                                            dataAdapter = new SqlDataAdapter(command);
                                            dataSet = new DataSet();
                                            dataAdapter.Fill(dataSet);
                                            count = dataSet.Tables[0].Rows.Count;
                                            //MessageBox.Show(count.ToString());

                                            if (count == 0)
                                            {
                                                query =
                                                    "INSERT INTO " +
                                                    "   test_level_mst " +
                                                    "   (   test_level_name" +
                                                    "   ,   test_idx" +
                                                    "   ,   insert_user" +
                                                    "   ,   update_user) " +
                                                    "VALUES" +
                                                    "   (" +
                                                    "       @test_level_name" +
                                                    "   ,   @test_idx" +
                                                    "   ,   1" +
                                                    "   ,   1" +
                                                    "   )";
                                                command = new SqlCommand(query, connection);
                                                command.Parameters.AddWithValue("@test_level_name", test_level_name);
                                                command.Parameters.AddWithValue("@test_idx", test_idx);
                                                result = command.ExecuteNonQuery();


                                                if (result == 1)
                                                {//정상 입력되었다면 인덱스 값을 가져온다.
                                                    query =
                                                "SELECT" +
                                                "   test_level_idx " +
                                                "FROM" +
                                                "   test_level_mst " +
                                                "WHERE" +
                                                "   test_level_name = @test_level_name "
                                                + " AND "
                                                + " test_idx = @test_idx "
                                                ;

                                                    command = new SqlCommand(query, connection);
                                                    command.Parameters.AddWithValue("@test_level_name", test_level_name);
                                                    command.Parameters.AddWithValue("@test_idx", test_idx);
                                                    dataAdapter = new SqlDataAdapter(command);
                                                    dataSet = new DataSet();
                                                    dataAdapter.Fill(dataSet);
                                                    count = dataSet.Tables[0].Rows.Count;
                                                    test_level_idx = (int)dataSet.Tables[0].Rows[0]["test_level_idx"];
                                                }
                                            }
                                            else
                                            {//해당 언어가 있는 경우 값을 세팅한다.
                                                test_level_idx = (int)dataSet.Tables[0].Rows[0]["test_level_idx"];
                                            }
                                        }
                                    }
                                }
                            }

                        }

                        int word_idx = 0;           //단어기본키
                        string word = "";           //단어
                        int word_dup_no = 0;        //중복단어번호
                        int word_day_info = 1;      //학습일차
                        result = 0;
                        //입력할 기본 값들이 다 정상인 경우만 사용한다.
                        if (lang_idx != 0 && test_group_idx != 0 && test_idx != 0 && test_level_idx != 0)
                        {

                            range = worksheet.Cells[j, 6];
                            //단어가 공백인 경우 건너뜀

                            if (null != range.Value && !range.Value.Equals(""))
                            {
                                //단어학습일자 worksheet.Cells[j, 5]
                                //단어 worksheet.Cells[j, 6]
                                //중복단어순번 worksheet.Cells[j, 8]
                                word = range.Value;
                                word = word.Trim();
                                //MessageBox.Show(word);


                                range = worksheet.Cells[j, 8];//중복단어순번 worksheet.Cells[j, 8]
                                if (null != range.Value && !range.Value.Equals(""))
                                {
                                    word_dup_no = (int)range.Value;
                                }


                                range = worksheet.Cells[j, 5];//단어학습일자 worksheet.Cells[j, 5]
                                if (null != range.Value && !range.Value.Equals(""))
                                {
                                    word_day_info = (int)range.Value;
                                }

                                String query =
                                    " SELECT"
                                    + "  word_idx"
                                    + " FROM"
                                    + "  word_mst"
                                    + " WHERE"
                                    + "  word = @word"
                                    + " AND "
                                    + "  word_dup_no = @word_dup_no"
                                    + " AND "
                                    + " test_level_idx = @test_level_idx"
                                    ;

                                SqlCommand command = new SqlCommand(query, connection);
                                command.Parameters.AddWithValue("@word", word);
                                command.Parameters.AddWithValue("@word_dup_no", word_dup_no);
                                command.Parameters.AddWithValue("@test_level_idx", test_level_idx);
                                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                                DataSet dataSet = new DataSet();
                                dataAdapter.Fill(dataSet);
                                int count = dataSet.Tables[0].Rows.Count;
                                //MessageBox.Show(count.ToString());

                                //중복단어가 없는 경우 -> 입력
                                if (count == 0)
                                {
                                    query =

                                     "INSERT INTO " +
                                     "   word_mst " +
                                     "   (   word" +
                                     "   ,   word_dup_no" +
                                     "   ,   test_level_idx" +
                                     "   ,   word_day_info" +
                                     "   ,   insert_user" +
                                     "   ,   update_user) " +
                                     "VALUES" +
                                     "   (" +
                                     "       @word" +
                                     "   ,   @word_dup_no" +
                                     "   ,   @test_level_idx" +
                                     "   ,   @word_day_info" +
                                     "   ,   1" +
                                     "   ,   1" +
                                     "   )";
                                    command = new SqlCommand(query, connection);
                                    command.Parameters.AddWithValue("@word", word);
                                    command.Parameters.AddWithValue("@word_dup_no", word_dup_no);
                                    command.Parameters.AddWithValue("@test_level_idx", test_level_idx);
                                    command.Parameters.AddWithValue("@word_day_info", word_day_info);
                                    result = command.ExecuteNonQuery();


                                    if (result == 1)
                                    {//정상 입력되었다면 인덱스 값을 가져온다.
                                        query =
                                      "SELECT"
                                    + "   word_idx "
                                    + "FROM"
                                    + "   word_mst "
                                    + " WHERE"
                                    + "  word = @word"
                                    + " AND "
                                    + "  word_dup_no = @word_dup_no"
                                    + " AND "
                                    + " test_level_idx = @test_level_idx"
                                    ;
                                        command = new SqlCommand(query, connection);
                                        command.Parameters.AddWithValue("@word", word);
                                        command.Parameters.AddWithValue("@word_dup_no", word_dup_no);
                                        command.Parameters.AddWithValue("@test_level_idx", test_level_idx);
                                        dataAdapter = new SqlDataAdapter(command);
                                        //MessageBox.Show(command.ToString());
                                        dataSet = new DataSet();
                                        dataAdapter.Fill(dataSet);
                                        count = dataSet.Tables[0].Rows.Count;
                                        //MessageBox.Show("count = " + count);
                                        word_idx = (int)dataSet.Tables[0].Rows[0]["word_idx"];
                                    }

                                    //발음기호 worksheet.Cells[j, 7]
                                    //단어품사 worksheet.Cells[j, 9]
                                    //단어뜻 worksheet.Cells[j, 10]
                                    //단어품사2 worksheet.Cells[j, 11]
                                    //단어뜻2 worksheet.Cells[j, 12]
                                    //단어품사3 worksheet.Cells[j, 13]
                                    //단어뜻3 worksheet.Cells[j, 14]
                                    //단어품사4 worksheet.Cells[j, 15]
                                    //단어뜻4 worksheet.Cells[j, 16]
                                    //단어품사5 worksheet.Cells[j, 17]
                                    //단어뜻5 worksheet.Cells[j, 18]
                                    //단어품사6 worksheet.Cells[j, 19]
                                    //단어뜻6 worksheet.Cells[j, 20]
                                    //단어품사7 worksheet.Cells[j, 21]
                                    //단어뜻7 worksheet.Cells[j, 22]


                                    range = worksheet.Cells[j, 7];//발음기호
                                    string word_pronounce = range.Value;
                                    for (int k = 9; k < 22; k += 2)
                                    {

                                        range = worksheet.Cells[j, k];//단어품사
                                        if (null != range.Value && !range.Value.Equals(""))
                                        {
                                            string word_parts = range.Value;

                                            range = worksheet.Cells[j, k + 1];//단어뜻
                                            string word_meaning = range.Text;

                                            query =
                                             "INSERT INTO " +
                                             "   word_dtl " +
                                             "   (   word_idx" +
                                             "   ,   word_parts" +
                                             "   ,   word_meaning" +
                                             "   ,   word_pronounce" +
                                             "   ,   insert_user" +
                                             "   ,   update_user) " +
                                             "VALUES" +
                                             "   (" +
                                             "       @word_idx" +
                                             "   ,   @word_parts" +
                                             "   ,   @word_meaning" +
                                             "   ,   @word_pronounce" +
                                             "   ,   1" +
                                             "   ,   1" +
                                             "   )";

                                            command = new SqlCommand(query, connection);
                                            command.Parameters.AddWithValue("@word_idx", word_idx);
                                            command.Parameters.AddWithValue("@word_parts", word_parts);
                                            command.Parameters.AddWithValue("@word_meaning", word_meaning);
                                            command.Parameters.AddWithValue("@word_pronounce", word_pronounce);
                                            result = command.ExecuteNonQuery();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    connection.Close();
                }
                //MessageBox.Show("단어 업로드가 완료되었습니다.");
                toolStripStatusLabelMain.Text = "업로드 완료";
                toolStripProgressBar.Value = 0;
                toolStripProgressBar.Visible = false;
                workbook.Close();
            }
        }

        /// <summary>
        /// 입력양식 내려받기 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 입력양식내려받기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel 통합문서|*.xlsx";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "엑셀 파일 저장";
                saveFileDialog.FileName = "Sample.xlsx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    String filepath = "";
                    if (!Path.HasExtension(saveFileDialog.FileName) || Path.GetExtension(saveFileDialog.FileName) != ".xlsx")
                    {
                        filepath = saveFileDialog.FileName + ".xlsx";
                    }
                    else
                    {
                        filepath = saveFileDialog.FileName;
                    }

                    if (filepath != null)
                    {
                        using (Stream resource = GetType().Assembly.GetManifestResourceStream("Main.Data.Sample.xlsx"))
                        {
                            if (resource == null)
                            {
                                throw new ArgumentException("해당 리소스 파일이 없음", "Main.Data.Sample.xlsx");
                            }
                            using (Stream output = File.OpenWrite(filepath))
                            {
                                resource.CopyTo(output);
                            }
                        }
                    }
                    toolStripStatusLabelMain.Text = "입력양식 내려받기 완료";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("에러 : " + ex.Message, "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 언어 콤보박스 선택 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboLoaded)
            {
                try
                {
                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand("SELECT * FROM test_group_mst WHERE lang_idx = @lang_idx", connection);
                    command.Parameters.AddWithValue("@lang_idx", comboBoxLanguage.SelectedValue.ToString());
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    connection.Close();
                    int count = dataSet.Tables[0].Rows.Count;
                    comboBoxTestGroup.DisplayMember = "test_group_name";
                    comboBoxTestGroup.ValueMember = "test_group_idx";
                    comboBoxTestGroup.DataSource = dataSet.Tables[0];
                    
                    comboBoxTestGroup.Show();
                    comboBoxTest.Hide();
                    comboBoxTestLevel.Hide();
                    flowLayoutPanelDay.Hide();
                    checkBoxAuto.Hide();
                    checkBoxAuto.Checked = false;
                    checkBoxBlink.Hide();
                    checkBoxBlink.Checked = false;
                    checkBoxRandom.Hide();
                    checkBoxRandom.Checked = false;
                    checkBoxRepeat.Hide();
                    checkBoxRepeat.Checked = false;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
        /// <summary>
        /// 시험그룹 콤보박스 선택 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxTestGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboLoaded)
            {
                try
                {
                    //MessageBox.Show("comboBoxTestGroup_SelectedIndexChanged");
                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand("SELECT * FROM test_mst WHERE test_group_idx = @test_group_idx", connection);
                    command.Parameters.AddWithValue("@test_group_idx", Int32.Parse(comboBoxTestGroup.SelectedValue.ToString()));
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    connection.Close();
                    int count = dataSet.Tables[0].Rows.Count;
                    comboBoxTest.DataSource = dataSet.Tables[0];
                    comboBoxTest.DisplayMember = "test_name";
                    comboBoxTest.ValueMember = "test_idx";

                    comboBoxTest.Show();
                    comboBoxTestLevel.Hide();
                    flowLayoutPanelDay.Hide();
                    checkBoxAuto.Hide();
                    checkBoxAuto.Checked = false;
                    checkBoxBlink.Hide();
                    checkBoxBlink.Checked = false;
                    checkBoxRandom.Hide();
                    checkBoxRandom.Checked = false;
                    checkBoxRepeat.Hide();
                    checkBoxRepeat.Checked = false;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
        /// <summary>
        /// 시험 콤보박스 선택 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxTest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboLoaded)
            {
                try
                {
                    //MessageBox.Show("comboBoxTest_SelectedIndexChanged");
                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand("SELECT * FROM test_level_mst WHERE test_idx = @test_idx", connection);
                    command.Parameters.AddWithValue("@test_idx", Int32.Parse(comboBoxTest.SelectedValue.ToString()));
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    connection.Close();
                    int count = dataSet.Tables[0].Rows.Count;
                    comboBoxTestLevel.DataSource = dataSet.Tables[0];
                    comboBoxTestLevel.DisplayMember = "test_level_name";
                    comboBoxTestLevel.ValueMember = "test_level_idx";
                    //if(count != 0)
                    comboBoxTestLevel.Show();
                    flowLayoutPanelDay.Hide();
                    checkBoxAuto.Hide();
                    checkBoxAuto.Checked = false;
                    checkBoxBlink.Hide();
                    checkBoxBlink.Checked = false;
                    checkBoxRandom.Hide();
                    checkBoxRandom.Checked = false;
                    checkBoxRepeat.Hide();
                    checkBoxRepeat.Checked = false;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
        /// <summary>
        /// 시험레벨 콤보박스 선택 변경
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxTestLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboLoaded)
            {
                try
                {
                    //플로우레이아웃패널내의 컨트롤 전체 삭제
                    flowLayoutPanelDay.Controls.Clear();

                    //MessageBox.Show(comboBoxTestLevel.SelectedValue.ToString());

                    //일자별 데이터 전체 호출
                    SqlConnection connection = new SqlConnection(connectionString);
                    SqlCommand command = new SqlCommand("SELECT DISTINCT word_day_info FROM word_mst WHERE test_level_idx = @test_level_idx ORDER BY word_day_info", connection);
                    command.Parameters.AddWithValue("@test_level_idx", Int32.Parse(comboBoxTestLevel.SelectedValue.ToString()));
                    connection.Open();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet);
                    connection.Close();
                    int count = dataSet.Tables[0].Rows.Count;
                    //MessageBox.Show(count.ToString());
                    for (int i = 0; i < count; i++)
                    {
                        Button button = new Button();
                        button.Tag = i;
                        button.Text = "Day " + dataSet.Tables[0].Rows[i]["word_day_info"].ToString();
                        button.Click += DayButton_Click;
                        flowLayoutPanelDay.Controls.Add(button);
                    }
                    flowLayoutPanelDay.Show();
                    checkBoxAuto.Hide();
                    checkBoxAuto.Checked = false;
                    checkBoxBlink.Hide();
                    checkBoxBlink.Checked = false;
                    checkBoxRandom.Hide();
                    checkBoxRandom.Checked = false;
                    checkBoxRepeat.Hide();
                    checkBoxRepeat.Checked = false;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }
        /// <summary>
        /// 데일리 버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DayButton_Click(object sender, EventArgs e)
        {
            checkBoxAuto.Checked = false;
            checkBoxBlink.Hide();
            checkBoxBlink.Checked = false;
            checkBoxRandom.Hide();
            checkBoxRandom.Checked = false;
            checkBoxRepeat.Hide();
            checkBoxRepeat.Checked = false;

            //listViewWordList.View = View.Details;
            richTextBoxWord.Text = "";

            var button = sender as Button;
            if (button != null)
            {
                //MessageBox.Show(button.Text.ToString().Replace("Day", "").Trim());
                string day = button.Text.ToString().Replace("Day", "").Trim();
                sheetName = button.Text.ToString();
                //일자별 데이터 전체 호출
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand("SELECT m.word, m.word_dup_no, m.word_idx, d.word_pronounce, d.word_parts, d.word_meaning FROM word_mst m INNER JOIN word_dtl d ON m.word_idx = d.word_idx WHERE m.test_level_idx = @test_level_idx AND m.word_day_info = @word_day_info ORDER BY m.word_idx;", connection);
                command.Parameters.AddWithValue("@test_level_idx", Int32.Parse(comboBoxTestLevel.SelectedValue.ToString()));
                command.Parameters.AddWithValue("@word_day_info", day);
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                connection.Close();
                int count = dataSet.Tables[0].Rows.Count;
                //MessageBox.Show(count.ToString());

                //리스트에 단어 데이터를 담는다.
                classWordlist = new List<ClassWord>();
                List<ClassWordDetail> classWordDetailList = new List<ClassWordDetail>();
                listViewWordList.Items.Clear();
                for (int i = 0; i < count; i++)
                {
                    ClassWord classWord;
                    //첫 순회의 경우 -1 하면 안됨
                    if (i == 0)
                    {
                        classWordDetailList = new List<ClassWordDetail>();
                        //MessageBox.Show(dataSet.Tables[0].Rows[i]["word"].ToString());

                        ClassWordDetail detail
                            = new ClassWordDetail(
                                Int32.Parse(dataSet.Tables[0].Rows[i]["word_idx"].ToString())
                                , dataSet.Tables[0].Rows[i]["word_parts"].ToString()
                                , dataSet.Tables[0].Rows[i]["word_meaning"].ToString()
                                , dataSet.Tables[0].Rows[i]["word_pronounce"].ToString()
                                );
                        classWordDetailList.Add(detail);
                        classWord
                            = new ClassWord(
                                Int32.Parse(dataSet.Tables[0].Rows[i]["word_idx"].ToString())
                                , dataSet.Tables[0].Rows[i]["word"].ToString()
                                , Int32.Parse(dataSet.Tables[0].Rows[i]["word_dup_no"].ToString())
                                , classWordDetailList
                            );
                        classWordlist.Add(classWord);

                    }
                    else
                    {//첫 순회가 아닌 경우
                        if (dataSet.Tables[0].Rows[i]["word_idx"].ToString().Equals(dataSet.Tables[0].Rows[i - 1]["word_idx"].ToString()))
                        {//앞순회의 단어랑 같은 단어인 경우 상세 내용만 추가하면 됨
                            classWord = classWordlist.Last();
                            ClassWordDetail detail
                            = new ClassWordDetail(
                                Int32.Parse(dataSet.Tables[0].Rows[i]["word_idx"].ToString())
                                , dataSet.Tables[0].Rows[i]["word_parts"].ToString()
                                , dataSet.Tables[0].Rows[i]["word_meaning"].ToString()
                                , dataSet.Tables[0].Rows[i]["word_pronounce"].ToString()
                                );
                            classWord.WordDetailList.Add(detail);
                            classWordlist[classWordlist.Count() - 1] = classWord;
                        }
                        else
                        {//새 단어인 경우

                            classWordDetailList = new List<ClassWordDetail>();
                            ClassWordDetail detail
                                = new ClassWordDetail(
                                    Int32.Parse(dataSet.Tables[0].Rows[i]["word_idx"].ToString())
                                    , dataSet.Tables[0].Rows[i]["word_parts"].ToString()
                                    , dataSet.Tables[0].Rows[i]["word_meaning"].ToString()
                                    , dataSet.Tables[0].Rows[i]["word_pronounce"].ToString()
                                    );
                            classWordDetailList.Add(detail);
                            classWord
                                = new ClassWord(
                                    Int32.Parse(dataSet.Tables[0].Rows[i]["word_idx"].ToString())
                                    , dataSet.Tables[0].Rows[i]["word"].ToString()
                                    , Int32.Parse(dataSet.Tables[0].Rows[i]["word_dup_no"].ToString())
                                    , classWordDetailList
                                );
                            classWordlist.Add(classWord);
                        }
                    }
                }

            }
            for (int i = 0; i < classWordlist.Count(); i++)
            {
                listViewWordList.Items.Add(classWordlist[i].Word).SubItems.Add(classWordlist[i].WordIdx.ToString());
                
            }
            if (classWordlist.Count() > 0)
            {
                checkBoxAuto.Show();
                listViewWordList.Items[0].Selected = true;

                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand("UPDATE user_mst SET last_level = @last_level WHERE user_email = @user_email", connection);
                command.Parameters.AddWithValue("@test_level_idx", Int32.Parse(comboBoxTestLevel.SelectedValue.ToString()));
                command.Parameters.AddWithValue("@user_email", userEmail);
                command.ExecuteNonQuery();
            }

            //MessageBox.Show("Message here");
        }

        /// <summary>
        /// 단어 리스트 선택 변경시 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewWordList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (listViewWordList.SelectedIndices.Count <= 0)
            {
                return;
            }
            int intselectedindex = listViewWordList.SelectedIndices[0];
            if (intselectedindex >= 0)
            {
                String text = listViewWordList.Items[intselectedindex].Text;
                richTextBoxWord.Clear();
                richTextBoxWord.SelectionFont = new Font("굴림", 70, FontStyle.Bold);
                richTextBoxWord.AppendText(text);
                richTextBoxWord.SelectionAlignment = HorizontalAlignment.Center;

                //MessageBox.Show(listViewWordList.Items[intselectedindex].SubItems[1].Text);
                //textBoxWord.Text = text;
                richTextBoxWord.Left = (this.ClientSize.Width - richTextBoxWord.Width) / 2;

                ClassWord classWord = classWordlist.Find(x => x.WordIdx == Int32.Parse(listViewWordList.Items[intselectedindex].SubItems[1].Text));

                List<ClassWordDetail> classWordDetaillist = classWord.WordDetailList;

                bool flag = true;
                foreach (ClassWordDetail details in classWordDetaillist)
                {
                    richTextBoxWord.SelectionFont = new Font("굴림", 40, FontStyle.Regular);
                    if (flag)
                    {
                        richTextBoxWord.AppendText("\n[ " + details.WordPronounce + " ]\n");
                        flag = false;
                    }

                    richTextBoxWord.SelectionFont = new Font("굴림", 20, FontStyle.Regular);
                    richTextBoxWord.AppendText("\n【" + details.WordParts + "】 " + details.WordMeaning);
                }

                //textBoxPronounce.Top = textBoxWord.Bottom;
                //textBoxPronounce.Left = (this.ClientSize.Width - textBoxPronounce.Width) / 2;
            }
        }
        /// <summary>
        /// 이전버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWordBefore_Click(object sender, EventArgs e)
        {
            if (listViewWordList.SelectedIndices[0] == 0)
            {
                listViewWordList.Items[listViewWordList.Items.Count - 1].Selected = true;
            }
            else
            {
                listViewWordList.Items[listViewWordList.SelectedIndices[0] - 1].Selected = true;
            }
        }
        /// <summary>
        /// 다음버튼 클릭 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonWordAfter_Click(object sender, EventArgs e)
        {
            if (listViewWordList.SelectedIndices[0] == listViewWordList.Items.Count - 1)
            {
                listViewWordList.Items[0].Selected = true;
            }
            else
            {
                listViewWordList.Items[listViewWordList.SelectedIndices[0] + 1].Selected = true;
            }
        }
        /// <summary>
        /// 자동 체크 박스 체크 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxAuto_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAuto.Checked)
            {
                //checkBoxBlink.Show();
                //checkBoxRandom.Show();
                checkBoxRepeat.Show();
            }
            else
            {
                checkBoxBlink.Hide();
                checkBoxBlink.Checked = false;
                checkBoxRandom.Hide();
                checkBoxRandom.Checked = false;
                checkBoxRepeat.Hide();
                checkBoxRepeat.Checked = false;
            }
        }
        /// <summary>
        /// 반복 체크 박스 체크 변경 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxRepeat_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxRepeat.Checked)
            {
                checkBoxBlink.Show();
                checkBoxRandom.Show();
            }
            else
            {
                checkBoxBlink.Hide();
                checkBoxBlink.Checked = false;
                checkBoxRandom.Hide();
                checkBoxRandom.Checked = false;
            }

        }
    }
}
