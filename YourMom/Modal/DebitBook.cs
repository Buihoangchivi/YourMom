using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

public class DebitBook : Category
{
	protected string startingDate;
	protected string endDate;
	protected double amount;
	protected string note;
	protected string stakeholder;

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

}
