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
	protected double moneyFund;
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
