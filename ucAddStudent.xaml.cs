using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WPFAN_Students
{
	/// <summary>
	/// Interaction logic for ucAddStudent.xaml
	/// </summary>
	public partial class ucAddStudent : UserControl
	{
		public ucAddStudent()
		{
			InitializeComponent();
		}

		private void ucAddStudent_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			if (!this.IsVisible) return;
			cbASSP.ItemsSource = DataInfo.ds.StudyProgrammes;
			cbASSP.DisplayMemberPath = "SPName";
			cbASSP.SelectedIndex = 0;
		}
		private void btASSave_Click(object sender, RoutedEventArgs e)
		{
			if (!ISCheck()) return;
			var SPRow = (cbASSP.SelectedValue as DataRowView)?.Row as
				dsStudents.StudyProgrammesRow;
			if (SPRow == null) return;
			DataInfo.ds.Students.AddStudentsRow(
				tbASId.Text, tbASName.Text, tbASEmail.Text, SPRow);
			MessageBox.Show("Data recorded!");
			tbASId.Text = "";
			tbASName.Text = "";
			tbASEmail.Text = "";
			cbASSP.SelectedIndex = 0;
		}

		private bool ISCheck()
		{
			return Validation.NameCheck(tbASName) && Validation.IdCheck(tbASId) &&
				Validation.emailCheck(tbASEmail);
		}
	}
}
