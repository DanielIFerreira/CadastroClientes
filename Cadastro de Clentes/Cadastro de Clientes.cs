using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Cadastro_de_Clentes
{
    public partial class Form1 : Form
    {
        //Paramentros para conexão com o banco de dados
        SqlConnection conexao;
        SqlCommand comando;
        SqlDataAdapter da;
        SqlDataReader dr;
        SqlConnection sqlCadastro = null;
        //Criando string de conexçao com o banco de dados
        private string strSql = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        //Ação do botão cadastrar novo cliente
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
           try
           {
                //Instanciando conexão com o banco de dados
                conexao = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=CadastroClientes;Data Source=DESKTOP-IGDV5DR");
                //Inserindo novo cliente no banco de dados 
                strSql = "INSERT INTO CadastroClientes (Nome, Email, Logradouro, Cep, Bairro, Numero, Cidade, Estado, Telefone) VALUES (@Nome, @Email, @Logradouro, @Cep, @Bairro, @Numero, @Cidade, @Estado, @Telefone)";
                comando = new SqlCommand(strSql, conexao);
                
                //Definindo variáveis
                comando.Parameters.Add("@Nome", SqlDbType.NVarChar).Value = txtNome.Text;
                comando.Parameters.Add("@Email", SqlDbType.NVarChar).Value = txtEmail.Text;
                comando.Parameters.Add("@Logradouro", SqlDbType.NVarChar).Value = txtLogradouro.Text;
                comando.Parameters.Add("@Cep", SqlDbType.NVarChar).Value = maskedTextBox1Cep.Text;
                comando.Parameters.Add("@Bairro", SqlDbType.NVarChar).Value = txtBairro.Text;
                comando.Parameters.Add("@Numero", SqlDbType.NVarChar).Value = txtNumero.Text;
                comando.Parameters.Add("@Cidade", SqlDbType.NVarChar).Value = txtCidade.Text;
                comando.Parameters.Add("@Estado", SqlDbType.NVarChar).Value = txtEstado.Text;
                comando.Parameters.Add("@Telefone", SqlDbType.NVarChar).Value = maskedTextBox1Telefone.Text;

                //Abrindo conexão com o banco de dados
                conexao.Open();
                comando.ExecuteNonQuery();

                //Exibindo mensgaem sucesso ao cadastrar
                MessageBox.Show("Cadastro realizado com sucesso!");

                //Limpando campos após cadastrar
                txtNome.Clear();
                txtEmail.Clear();
                txtLogradouro.Clear();
                maskedTextBox1Cep.Clear();
                txtBairro.Clear();
                txtNumero.Clear();
                txtCidade.Clear();
                txtEstado.Clear();
                maskedTextBox1Telefone.Clear();
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
           finally
           {
                //Fechando conexão com o banco de dados
                conexao.Close();
                conexao = null;
                comando = null;
           }
        }

        //Ação do botão atualizar listagem de clientes
        private void btnListar_Click(object sender, EventArgs e)
        {

            try
            {
                //Instanciando conexão com o banco de dados
                conexao = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=CadastroClientes;Data Source=DESKTOP-IGDV5DR");
                //Buscando registros no banco de dados e ordenando
                strSql = "SELECT * FROM CadastroClientes ORDER BY Id ASC";
             
                DataSet ds = new DataSet();
                da = new SqlDataAdapter(strSql, conexao);
                da.Fill(ds);
                dgvLIstar.DataSource = ds.Tables[0];

                //Abrindo conexão com o banco de dados
                conexao.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //Fechando conexão com o banco de dados
                conexao.Close();
                conexao = null;
                comando = null;
            }
        }

        //Ação do botão excluir cliente
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //Mensagem de confirmação de exlcusão de  registro
            if (MessageBox.Show($"Deseja realmente excluir?", "Exlcuir", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)   
            {
                MessageBox.Show("Operação cancelada");
            }
            else
            {
                try
                {
                    //Instanciando conexão com o banco de dados
                    conexao = new SqlConnection(@"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=CadastroClientes;Data Source=DESKTOP-IGDV5DR");
                    //Deletando cliente informado do banco de dados, recebendo como parametro o ID
                    strSql = "DELETE  CadastroClientes WHERE Id = @Id ";
                    comando = new SqlCommand(strSql, conexao);
                    //Passando ID informado pelo usuario como parametro para o banco de dados
                    comando.Parameters.AddWithValue("@Id", txtId.Text);

                    //Abrindo conexão com o banco de dados
                    conexao.Open();
                    comando.ExecuteNonQuery();

                    //Exibindo mensgaem sucesso ao excluir
                    MessageBox.Show("Cadastro excluido com sucesso.");

                    //Limpando campos após cadastrar
                    txtId.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    //Fechando conexão com o banco de dados
                    conexao.Close();
                    conexao = null;
                    comando = null;
                }
            }
        }

        //Inserindo o cursor no inicio do campo CEP
        private void maskedTextBox1Cep_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        { 
            maskedTextBox1Cep.SelectionStart = 0;
        }

        //Inserindo o cursor no inicio do campo Telefone
        private void maskedTextBox1Telefone_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            maskedTextBox1Telefone.SelectionStart = 0;
        }   
    }   
}
