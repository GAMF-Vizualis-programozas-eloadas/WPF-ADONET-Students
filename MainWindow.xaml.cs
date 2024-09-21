using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using WPFAN_Students.dsStudentsTableAdapters;

namespace WPFAN_Students
{
	public partial class MainWindow : Window
	{
		taStudents taS;
		taStudyProgrammes taSP;
		TableAdapterManager tam;
		public MainWindow()
		{
			InitializeComponent();
			DataInfo.ds = new dsStudents();
			string cs = ConfigurationManager.ConnectionStrings["csStudents"].ConnectionString;
			SqlConnection scConnection = new SqlConnection(cs);
			taS = new taStudents
			{
				Connection = scConnection
			};
			taSP = new taStudyProgrammes
			{
				Connection = scConnection
			};
			tam = new TableAdapterManager 
			{ 
				taStudents = taS, 
				taStudyProgrammes = taSP 
			};
			taS.Fill(DataInfo.ds.Students);
			taSP.Fill(DataInfo.ds.StudyProgrammes);
		}
		// File menu
		private void miSave_Click(object sender, RoutedEventArgs e)
		{
			tam.UpdateAll(DataInfo.ds);
		}

		private void miExit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		// Query menu
		private void miAllData_Click(object sender, RoutedEventArgs e)
		{
			CollapseAll();
			var er = from x in DataInfo.ds.Students
							 join y in DataInfo.ds.StudyProgrammes
								 on x.StudyProgramme equals y.Id
							 select new
							 {
								 x.Id,
								 x.Name,
								 x.email,
								 y.SPName,
								 y.Type
							 };
			dgAllData.ItemsSource = er.ToList();
			dgAllData.Visibility = Visibility.Visible;
		}

		private void miEmailList_Click(object sender, RoutedEventArgs e)
		{
			CollapseAll();
			var er = (from x in DataInfo.ds.Students
								select x.email);
			var s = er.Aggregate("", (current, x) => current + x + "\r\n");
			tbEmailList.Text = s;
			tbEmailList.Visibility = Visibility.Visible;
		}
		private void CollapseAll()
		{
			dgAllData.Visibility = Visibility.Collapsed;
			tbEmailList.Visibility = Visibility.Collapsed;
			ucAddStudent.Visibility = Visibility.Collapsed;
			ucModifyStudent.Visibility = Visibility.Collapsed;
			ucAddStudyProgramme.Visibility = Visibility.Collapsed;
			ucModifyStudyProgramme.Visibility = Visibility.Collapsed;
		}

		// Add/Modify menu
		private void miAddModify_GotFocus(object sender, RoutedEventArgs e)
		{
			if (DataInfo.ds.Students.Rows.Count < 1)
				miModifyStudent.IsEnabled = false;
			else
				miModifyStudent.IsEnabled = true;
			if (DataInfo.ds.StudyProgrammes.Rows.Count < 1)
			{
				miModifySP.IsEnabled = false;
				miAddStudent.IsEnabled = false;
			}
			else
			{
				miModifySP.IsEnabled = true;
				miAddStudent.IsEnabled = true;
			}
		}

		private void miStudentDataAdd_Click(object sender, RoutedEventArgs e)
		{
			CollapseAll();
			ucAddStudent.Visibility = Visibility.Visible;
		}

		private void miStudentDataModify_Click(object sender, RoutedEventArgs e)
		{
			CollapseAll();
			ucModifyStudent.Visibility = Visibility.Visible;
		}

		private void miStudyProgrammeAdd_Click(object sender, RoutedEventArgs e)
		{
			CollapseAll();
			ucAddStudyProgramme.Visibility = Visibility.Visible;
		}

		private void miStudyProgrammeModify_Click(object sender, RoutedEventArgs e)
		{
			CollapseAll();
			ucModifyStudyProgramme.Visibility = Visibility.Visible;
		}
	}
}