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
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace Main
{
    public partial class FormMain : Form
    {
        public string userName = "";
        public string userEmail = "";
        public string userAuth = "USER";
        public int last_test_level = 0; //마지막 공부한 위치
        int test_idx = 0;
        int test_group_idx = 0;
        int lang_idx = 0;
        string connectionString = @"Data Source=leedohyun.asuscomm.com,1433;Initial Catalog=DMemorizer;User ID=sa;Password=P@ssw0rd;";
        public FormMain()
        {
            InitializeComponent();
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
                    setComboBoxLanguage();

                    labelName.Text = userName;
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
                    setComboBoxLanguage();
                    labelName.Text = userName;
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

            buttonAdmin.Hide();
            buttonLogOut.Hide();
            menuStripMain.Hide();
            buttonLogin.Show();
            buttonSignUp.Show();
            flowLayoutPanelCombo.Hide();
            toolStripStatusLabelMain.Text = "로그아웃 성공";
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

                    object value = dataSet.Tables[0].Rows[0]["last_level"];

                    if (value != DBNull.Value)
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

                    //콤보박스 설치
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

        /***********************************************************************************
        이벤트함수           
        ************************************************************************************/

        /// <summary>
        /// 화면 로드 시 동작
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormMain_Load(object sender, EventArgs e)
        {

        }
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
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel 통합문서|*.xlsx";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.Title = "엑셀 파일 저장";
                saveFileDialog.FileName = "기본이름";   //기본 이름 설정
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

                        worksheet.Name = "테스트";                             // 시트 이름 설정
                        Excel.Range range = worksheet.Cells[1, 1];               //입력 범위 지정
                        range.Value = "테스트입력";                              //입력 값 설정

                        //Excel.Worksheet worksheet = workbook.Worksheets.Add();  //새 시트 추가

                        MessageBox.Show("저장되었습니다.", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);//내용, 제목, 확인 버튼, 가운데 이모티콘
                        workbook.SaveAs(filepath);
                        workbook.Close();
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
                                                "   test_level_name = @test_level_name ";

                                            command = new SqlCommand(query, connection);
                                            command.Parameters.AddWithValue("@test_level_name", test_level_name);
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
                                                "   test_level_name = @test_level_name ";

                                                    command = new SqlCommand(query, connection);
                                                    command.Parameters.AddWithValue("@test_level_name", test_level_name);
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
    }
}
