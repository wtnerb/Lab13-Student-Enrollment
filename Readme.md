
# Student Enrollment
This web app acts like a registrar's view of student enrollment lists.
Deployment lives at: http://studentenrollmentapp.azurewebsites.net/
## Overview
It is assumed any user is authorized and allowed to add/create/delete/modify courses and students at a school. The state of the school is persisted in a database. Student and course data is stored seperately for the sake of memory efficiency. 

Note: There is no security in this app. There is no restore to previous state. DO NOT USE THIS FOR YOUR JOB! Any consequences or losses incurred, including but not limited to loss of data or stolen data, is the sole responsibility of the end user for trusting a student's homework assignment in a professional setting.

## Use
As is, this web app is not ready for use. However, a few customization options and you can take this app and customize it for your own use.

Step one is to download the source code from github and open it in a text editor. Then:

- replace the image link in the _Layout.cshtml file (located in Views/Shared) with the desired logo of your organization. Conveniently, it has an ID of logo.

At that point. you are mostly ready to roll. Get the App running and it will use a local Database. IF you want multiple computers to access the same database, either get one running on a server or deploy to Azure. Use Bing to find one of the many tutorials for deployment.

Last but not least, use the Create, Edit, and Delete options to make your model adequately represent the state of your school and students.

## Architecture
- MVC
- Xunit

## Version
2018-04-15 v0.9 Some styling, database works when manipulated manually, database displays when manipulated to have data, Models are tested. No seeding, No working CRUD, Limited website navigation, Limited styling.
