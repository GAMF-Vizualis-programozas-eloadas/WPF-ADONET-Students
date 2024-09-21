using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WPFAN_Students
{
	public class Validation
	{
		public static bool emailCheck(TextBox tbEmail)
		{
			if (!Regex.IsMatch(tbEmail.Text,
				@"^([ \w-\.]+)@(( \[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|" + @"(([ \w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
			{
				MessageBox.Show("Invalid e-mail address!");
				return false;
			}
			return true;
		}

		public static bool IdCheck(TextBox tbId)
		{
			if (tbId.Text.Length == 0)
			{
				MessageBox.Show("Id is a compulsory field!");
				return false;
			}
			if (tbId.Text.Length != 6)
			{
				MessageBox.Show("The length of the student ID is 6 characters!");
				return false;
			}

			if (tbId.Text.ToCharArray().Any(
				c => !Char.IsLetter(c) && !Char.IsDigit(c)
				))
			{
				MessageBox.Show("The Id can contain only letters and digits!");
				return false;
			}
			var er = from x in DataInfo.ds.Students
							 where x.Id == tbId.Text
							 select x.Id;
			if (er.Any())
			{
				MessageBox.Show("The given Id is already in the database!");
				return false;
			}
			return true;
		}

		public static bool NameCheck(TextBox tbName)
		{
			if (tbName.Text.Length == 0)
			{
				MessageBox.Show("Name is a compulsory field!");
				return false;
			}
			if (tbName.Text.ToCharArray().Any(c => !Char.IsLetter(c) && c != ' ' && c != '-'
				))
			{
				MessageBox.Show("The name can contain only letters and spaces!");
				return false;
			}
			return true;
		}
	}
}
