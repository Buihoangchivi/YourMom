using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

public class DetailCategory : Category
{
	protected double amount;

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

}

