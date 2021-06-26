using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

public class Report : INotifyPropertyChanged
{
	protected string id;
	protected string startingDate;
	protected string endDate;
	protected List<DetailCategory> income;
	protected List<DetailCategory> expense;
	protected List<DetailCategory> debt;
	protected List<DetailCategory> loan;

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

	public string StartingDate
	{
		get
		{
			return startingDate;
		}
		set
		{
			startingDate = value;
			OnPropertyChanged("StartingDate");
		}
	}

	public string EndDate
	{
		get
		{
			return endDate;
		}
		set
		{
			endDate = value;
			OnPropertyChanged("EndDate");
		}
	}

	public List<DetailCategory> Income
	{
		get
		{
			return income;
		}
		set
		{
			income = value;
			OnPropertyChanged("Income");
		}
	}

	public List<DetailCategory> Expense
	{
		get
		{
			return expense;
		}
		set
		{
			expense = value;
			OnPropertyChanged("Expense");
		}
	}

	public List<DetailCategory> Debt
	{
		get
		{
			return debt;
		}
		set
		{
			debt = value;
			OnPropertyChanged("Debt");
		}
	}

	public List<DetailCategory> Loan
	{
		get
		{
			return loan;
		}
		set
		{
			loan = value;
			OnPropertyChanged("Loan");
		}
	}

	#region INotifyPropertyChanged Members  

	public event PropertyChangedEventHandler PropertyChanged;
	private void OnPropertyChanged(string propertyName)
	{
		if (PropertyChanged != null)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	#endregion

}
