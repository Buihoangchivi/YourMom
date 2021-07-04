using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

public class Transaction : INotifyPropertyChanged
{
	protected string id;
	protected string date;
	protected string transactionType;
	protected double amount;
	protected string note;
	protected string stakeholder;

	public string ID
	{
		get
		{
			return id;
		}
		set
		{
			id = value;
			OnPropertyChanged("ID");
		}
	}

	public string Date
	{
		get
		{
			return date;
		}
		set
		{
			date = value;
			OnPropertyChanged("Date");
		}
	}

	public string TransactionType
	{
		get
		{
			return transactionType;
		}
		set
		{
			transactionType = value;
			OnPropertyChanged("TransactionType");
		}
	}

	public double Amount
	{
		get
		{
			return amount;
		}
		set
		{
			amount = value;
			OnPropertyChanged("Amount");
		}
	}

	public string Note
	{
		get
		{
			return note;
		}
		set
		{
			note = value;
			OnPropertyChanged("Note");
		}
	}

	public string Stakeholder
	{
		get
		{
			return stakeholder;
		}
		set
		{
			stakeholder = value;
			OnPropertyChanged("Stakeholder");
		}
	}

	#region INotifyPropertyChanged Members  

	public event PropertyChangedEventHandler PropertyChanged;
	protected void OnPropertyChanged(string propertyName)
	{
		if (PropertyChanged != null)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	#endregion

}
