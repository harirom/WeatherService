WeatherService

Developed in VS2017 using .Net Core framework.

Weather service uses OpenWeather API and fethch the Weather Report of different cities as per the provided input in a file with city name and city id.

Its is an ASP.Net Core MVC Application which can be accessd through the web browser.

Necessary things to do before running the Application:

	Must have created a destination folder as "D:\Weather Info\destination" (or) you can set your own destination path in the configuration 	        settings inside "appsettings.json" file by changing the value of "DestinationDir". 

Execution Instructions:

	Step 1 : Must open the Application as Adminstrator and run the application in IIS which will take you to the web browser and you will be asked to 		 choose the file with City Info from anywhere in your machine. A sample "CityList.txt" can be find in the code repository for file                  structure reference.
	Step 2 : Once the file is choosed, You can click on "Generate Report" button which will gives you a info popup about report generation with current 		 date and time. So that you will go and check the report generated in Json format for the respective cities inside the destination folder.

	Inorder to store the historical information, The files are stored inside the folder name with current timestamp.


Note : I have also deployed this application in Azure which can be accessed through (https://weatherservice20190803.azurewebsites.net/). By doing this, Going further, we can store the wearther reports in the Azure Kudu platform itself where we generally store the logs insted of storing the info in our local machine.


	

			