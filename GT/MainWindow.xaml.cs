using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GT
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}
		Window1 wind1 = new Window1();
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Main main = new Main();
			if (LoginBox.Text == "")
			{
				MessageBox.Show("Введите значения!", "Ошибка");
			}
			else
			{
				if (passbox.Password.ToString()== "")
				{
					MessageBox.Show("Введите значения!", "Ошибка");
				}
				else
				{
					string path = @"Data Source=BASS\SQLEXPRESS; Initial catalog = Guitar_tutorial_DB; Integrated Security=True";
					SqlConnection connection = new SqlConnection(path);
					connection.Open();
					string login = LoginBox.Text;
					if (login.Length > 20)
					{
						MessageBox.Show("Слишком длинный логин!", "Неверный ввод");

					}
					else
					{
						string password = passbox.Password.ToString();
						DataTable dt = new DataTable();
						string command = $"SELECT * FROM [dbo].[authorization] WHERE [login] LIKE @login";
						SqlCommand sqlCommand = new SqlCommand(command, connection);
						sqlCommand.Parameters.AddWithValue("login", login);
						SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
						dt = new DataTable("[dbo].[authorization]");
						dataAdapter.Fill(dt);
						if (dt.Rows.Count > 0)
						{
							MessageBox.Show("Пользователь уже есть в базе данных!", "Не удалось зарегестрироваться!");
						}
						else
						{
							command = $"INSERT INTO [dbo].[authorization] ([login], [password]) VALUES (@login, @password)";

							sqlCommand = new SqlCommand(command, connection);

							sqlCommand.Parameters.AddWithValue("login", login);
							sqlCommand.Parameters.AddWithValue("password", password);
							sqlCommand.ExecuteNonQuery();
							connection.Close();
							main.Owner = this;
							this.Hide();
							main.ShowDialog();
						}

					}
				}
			}
		}
		
		public string login;
        private void sign_in_Click(object sender, RoutedEventArgs e)
        {
			string path = @"Data Source=BASS\SQLEXPRESS; Initial catalog = Guitar_tutorial_DB; Integrated Security=True";
			SqlConnection connection = new SqlConnection(path);
			DataTable dt = new DataTable();
			connection.Open();
			login = LoginBox.Text;
			string password = passbox.Password.ToString();

			string command = $"SELECT * FROM [dbo].[authorization] WHERE ([login] LIKE '{login}') AND ([password] LIKE '{password}')";
			SqlCommand sqlCommand = new SqlCommand(command, connection);
			SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
			dt = new DataTable("[dbo].[authorization]");
			dataAdapter.Fill(dt);
			if (dt.Rows.Count > 0)
			{
				Main main = new Main();
				main.Owner = this;
				this.Hide();
				main.ShowDialog();
				
			}
			else
			{
				MessageBox.Show("Проверьте свой логин или пароль", "Неверный логин или пароль!");

			}
			sqlCommand.ExecuteNonQuery();
			connection.Close();
		}
    }
}
