This is a Complaint Application, it is designed based on the Onion Architicture.

**NOTES: 1. To Run the Application please make sure that the UI and API are the startup projects**

**NOTES: 2. To LOGIN as an admin => Email:Admin@admin.com password:Admin123**

**NOTES: 3. To LOGIN as an User => you can create User**


It has 2 main modules (App, UI).

Modules:
  1. API:
     - This is a webapi module that handles requests and returns responses.
     - This module is connected to another 2 modules which are (Data, DTO):
       
         1.1.1: Data: It is module that has all the models stored in the database as proxies.
       
         1.1.2: DTO: It the a module that has all the models that need to be sent to User Interface (UI).
       
       - The API module maps the data from DTO to Entity vice versa.
         
         1.2.1: when the data needs to be stored the database the api maps it from DTO to Data.
         
         1.2.2: when the data is retrieved from the database the api maps it from Data to DTO.
         
       - The Api is connected to 2 other modules named "Repository" and "Service" which has all the functionalities to manipulate the data in the database.
         
  2. UI :
     
     - This is MVC web application which is responsible of handling the requests from the browser to the api vice versa.
       
     - it has 3 controllers:
       
       Controllers:
     
         2.1: Home Controller: this is the main page.
         2.2  User Controller: 
            2.2.1 This Controller in Responsible of the user registration and user login using the Email and Password.
            2.2.2 The User Controller grants the user the ability to modify its information, except the Email.
         
         2.3: Complaint Controller: this is controllers that has the functionalitites of all CRUD Operations for the complaints.
       
           2.3.1: Views:
       
               2.3.1.1: Index: this view displays the complaints.
                  2.3.1.1.1 If the user logged in has the type "User", then, only his complaints will be shown for him, also, the user can modify the complaint whenever he wants.
                  2.3.1.1.2 If the user logged in has the type "Admin", then, All the complaints in the system are going to be displayed in the view, and the user can only do the following actions: 
                    a. Approve the Complaint.
                    b. Reject the Complaint.
                    c. View the Complaint and its demands.
       
               2.3.1.2: AddEdit: this is the view that has the complaint form which allows the user to submit a complaint. This view also has a partialview for the demands.
      **Note: you need to press the "plus" icon to add the demands into the table below inorder to be saved as demands for that complaint.**


