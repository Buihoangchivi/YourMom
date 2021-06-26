using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

public class Budget : Category
{
	protected string startingDate;
	protected string endDate;
	protected int daysLeft;
	protected double moneyFund;
	protected double balance;
	protected double progress;
	protected string note;

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

	public int DaysLeft
	{
		get
		{
			return daysLeft;
		}
		set
		{
			daysLeft = value;
			OnPropertyChanged("DaysLeft");
		}
	}

	public double MoneyFund
	{
		get
		{
			return moneyFund;
		}
		set
		{
			moneyFund = value;
			OnPropertyChanged("MoneyFund");
		}
	}

	public double Balance
	{
		get
		{
			return balance;
		}
		set
		{
			balance = value;
			OnPropertyChanged("Balance");
		}
	}

	public double Progress
	{
		get
		{
			return progress;
		}
		set
		{
			progress = value;
			OnPropertyChanged("Progress");
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

}
