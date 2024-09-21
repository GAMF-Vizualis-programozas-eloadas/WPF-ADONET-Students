using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace WPFAN_Students
{
	/// <summary>
	/// Interaction logic for ucModifyStudent.xaml
	/// </summary>
	public partial class ucModifyStudent : UserControl
	{
		public ucModifyStudent()
		{
			InitializeComponent();
		}

		private void ucModifyStudent_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!IsVisible) return;
			cbMSId.ItemsSource = DataInfo.ds.Students;
			cbMSId.DisplayMemberPath = "Id";
			cbMSSP.ItemsSource = DataInfo.ds.StudyProgrammes;
			cbMSSP.DisplayMemberPath = "SPName";
			cbMSId.SelectedIndex = 0;
		}

		private void cbMSId_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var hr = (cbMSId.SelectedValue as DataRowView)?.Row
				as dsStudents.StudentsRow;
			if (hr == null) return;
			tbMSName.Text = hr.Name;
			tbMSEmail.Text = hr.email;
			for (int i = 0; i < cbMSSP.Items.Count; i++)
			{
				if ((dsStudents.StudyProgrammesRow)
					((DataRowView)cbMSSP.Items[i]).Row != hr.StudyProgrammesRow)
					continue;
				cbMSSP.SelectedIndex = i;
				break;
			}
		}

		private void btMSSave_Click(object sender, RoutedEventArgs e)
		{
			if (!MSCheck()) return;
			var hr = (cbMSId.SelectedValue as DataRowView)?.Row
				as dsStudents.StudentsRow;
			if (hr == null) return;
			hr.BeginEdit();
			hr.Name = tbMSName.Text;
			hr.email = tbMSEmail.Text;
			hr.StudyProgrammesRow = (cbMSSP.SelectedValue as DataRowView)?.Row
				as dsStudents.StudyProgrammesRow;
			hr.EndEdit();
			MessageBox.Show("Data recorded!");
		}
		private bool MSCheck()
		{
			return Validation.NameCheck(tbMSName) &&
				Validation.emailCheck(tbMSEmail);
		}

	}
}
