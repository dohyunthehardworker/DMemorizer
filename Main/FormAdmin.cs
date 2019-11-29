using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Main
{
    public partial class FormAdmin : Form
    {
        public FormAdmin()
        {
            InitializeComponent();
        }

        string connectionString = @"Data Source=leedohyun.asuscomm.com,1433;Initial Catalog=DMemorizer;User ID=sa;Password=P@ssw0rd;";
        private SqlConnection connection;
        private SqlDataAdapter sqlDataAdapter;

        private void dataGridViewAdmin_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dataTable = ((DataTable)dataGridViewAdmin.DataSource).GetChanges();

            if (dataTable != null)
            {
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(sqlDataAdapter);
                sqlDataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                sqlDataAdapter.Update(dataTable);
                ((DataTable)dataGridViewAdmin.DataSource).AcceptChanges();
                MessageBox.Show("수정되었습니다.");
            }
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException exception)
            {
                switch (exception.Number)
                {
                    case 0:
                        MessageBox.Show("서버에 접속이 되지 않습니다. 잠시후 다시 시도해 주십시오.");
                        break;
                    case 1045:
                        MessageBox.Show("사용자명과 패스워드가 일치하지 않습니다.");
                        break;
                    default:
                        MessageBox.Show(exception.Message);
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException exception)
            {
                MessageBox.Show(exception.Message);
                return false;
            }
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {

            connection = new SqlConnection(connectionString);

            if (this.OpenConnection() == true)
            {
                string sql =
                "SELECT " +
                    "user_idx AS '순번'" +
                    ", user_email AS '이메일'" +
                    //", user_password AS 비밀번호" +
                    ", user_name AS '사용자명'" +
                    ", user_use_flag AS '사용여부'" +
                    ", user_auth AS '사용자권한'" +
                    ", user_email_checked AS '이메일인증여부'" +
                    //", insert_user AS " +
                    ", insert_timestamp AS '가입일자'" +
                " FROM" +
                "   user_mst";

                sqlDataAdapter = new SqlDataAdapter(sql, connection);
                
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                dataGridViewAdmin.DataSource = dataSet.Tables[0];
                this.CloseConnection();
            }

        }
    }
}
