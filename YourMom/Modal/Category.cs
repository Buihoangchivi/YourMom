using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

public class Category : INotifyPropertyChanged
{
	protected string id;
	protected string name;
	protected string imagePath;

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

	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
			OnPropertyChanged("Name");
		}
	}

	public string ImagePath
	{
		get
		{
			return imagePath;
		}
		set
		{
			imagePath = value;
			OnPropertyChanged("ImagePath");
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
