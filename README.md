# EduPartners

## Authors
Nirmaha Mukherjee and Sean Ryan

## Missouri State Competition

### Prompt
Create a program for your school’s Career and Technical Education Department to collect and store information about local businesses and community partners. The program should cover details for at least 25 different partners, including the type of organization, available resources, and direct contact information for an individual. Users should be able to search and filter the information as needed.

### Stack
* **Front-end:** WPF (Windows Presentation Foundation) with XAML forms for UI. We used the WPF Modern Vertical Menu template by [Jeyderht](https://github.com/Jeyderht/WPFModernVerticalMenu).
* **Back End:** C# and other dotnet Core libraries.
* **Database:** MongoDB Clusters.

### Requirements
- Administrators can log in to an account, with each administrator linked to a school.
- Administrators can add partners and track their roles.

## File Structure
```
D:.
├───Assets
├───bin
│   └───Debug
├───Core
├───MVVM
│   ├───Model
│   ├───View
│   │   ├───Controls
│   │   └───Pages
│   └───ViewModel
├───Properties
├───Resources
├───Styles
└───Themes
```

## Model
### User Model
```cs
public class User
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string Id { get; set; }

   [BsonRequired]
   public string Name { get; set; }

   [BsonRequired]
   public string Email { get; set; }

   public string About { get; set; }

   public string PhoneNumber { get; set; }

   [BsonRequired]
   public string Password { get; set; }

   public string ProfileImage { get; set; }

   [BsonRequired]
   public School HomeSchool { get; set; }
}
```
In the user model, the fields are:
- Name -- The name of the user
- Email -- Email of the user
- About -- The description of the user's profile
- Phone number -- The user's phone number
- Password -- The user's encrypted password
- Profile image -- The user's profile image
- Homeschool -- The school the user is administrating

### School Model
```cs
public class School
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRequired]
    public string Name { get; set; }

    [BsonRequired]
    public string Address { get; set; }

    [BsonRequired]
    public string Code { get; set; }

    [BsonRequired]
    public string City { get; set; }

    [BsonRequired]
    public string State { get; set; }

    [BsonRequired]
    public string Zip { get; set; }

    public Optional<List<Partner>> Partners { get; set; } = new Optional<List<Partner>>();
   
}
```
In the school model, the fields are:
- Name -- The name of the school
- Address -- The location of the school phone
- State -- The state the school is in
- City -- The city the school is in
- Zip -- The zip code of the city
- Code -- The security code generated for the school
- Partners -- The List of Partners that finance the school

### Partner Model
```cs
public class Partner
{
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string Id { get; set; }

   [BsonRequired]
   public string Name { get; set; }

   [BsonRequired]
   public string Description { get; set; }

   [BsonRequired]
   public string ResourcesAvailable { get; set; }

   [BsonRequired]
   public string Industry { get; set; }

   [BsonRequired]
   public DateTime StartDate { get; set; }

   [BsonRequired]
   public string RepresentativeName { get; set; }

   [BsonRequired]
   public string RepresentativeEmail { get; set; }

   public string RepresentativePhoneNumber { get; set; } // Optional
   public string Website { get; set; } // Optional
   public string Address { get; set; } // Optional

   [BsonRequired]
   public double Savings { get; set; }
}
```
In the partner model, the fields are:
- Name -- The name of the company
- Description -- What the company does
- Resources Available -- The resources the company is providing
- Industry -- The industry the company is in
- Start Date -- The day the partnership started
- Representative Name -- The name of the company representative
- Representative Email -- The email of the company representative
- Representative Phone Number -- The phone number of the company representative
- Website -- The company's website
- Address -- The company's full address
- Saving -- The amount of money the company is saving the school per year

## View Controls
### Home Page
The starting page of the application is where the user can log in, sign up, create a school, or get help.
### Log In
This is where the user can log into their account.
### Sign Up
This is where a user can make an account that will need to connect to a school via a code.
### Create School
This allows the user to make a new school and get a school code.
### Help
This page guides the user through each step of the log in, sign up, and creating a school.

## Views Pages
### Dashboard
This is the main page, accessible once the user logs in. It contains three graphs that display partner data for administrators to analyze. It also includes navigation to every other page on the site.
### View Partners
This is where users can view their partner's information.
### Add Partner
This is where users can add new partners.
### Edit Partner
This is where users can edit existing partners.
### Profile
This is where the user can edit their information, change their password, and their profile image.
### Help
This is where the user can find detailed instructions about each page.
### Logout
This is where the user can log out.

## References

#### Tools Used
- **Visual Studio:**
  - https://visualstudio.microsoft.com/
- **.Net Framework:**
  - https://dotnet.microsoft.com/en-us/download/dotnet-framework
- **MongoDB Altas:**
  - https://www.mongodb.com/atlas/database
- **MongoDB Compass:**
  - https://www.mongodb.com/products/tools/compass

#### Libraries
- **Nuget Pack Manager:**
  - https://learn.microsoft.com/en-us/nuget/
- **Bcrypt:**
  - https://github.com/BcryptNet/bcrypt.net
- **MongoDB:**
  - https://www.mongodb.com/docs/
- **Data Vizualtion Charting:**
  - https://www.nuget.org/packages/DotNetProjects.WpfToolkit.DataVisualization/6.1.94

#### Resources
- **Dashboard Template:**
  - https://github.com/Jeyderht/WPFModernVerticalMenu
- **Login Page Background Image:**
  - https://www.freepik.com/free-photo/business-people-shaking-hands-together_2975988.htm#query=business&position=2&from_view=keyword&track=sph&uuid=dbc8f5b0-e242-4b1e-948d-f4c38f3b2b97
- **Signup Page Background Image:**
  - https://www.freepik.com/free-photo/still-life-documents-stack_95018693.htm#query=Business%20Files&position=48&from_view=search&track=ais&uuid=e4b5bf5b-d784-49f7-a6da-8610c6800e8f
- **School Selection Background Image:**
  - https://www.freepik.com/free-photo/empty-classroom-due-coronavirus-pandemic_26606670.htm#query=classroom&position=4&from_view=keyword&track=sph&uuid=144bac40-d037-45bc-8c53-b22bce6b562a
- **App Icon:**
  - https://www.flaticon.com/free-icon/customer-engagement_13339260?term=handshake&page=1&position=14&origin=search&related_id=13339260
- **Delete Icon:**
  - https://www.flaticon.com/free-icon/set_13487001?term=trashcan&page=1&position=23&origin=search&related_id=13487001
- **Logout Icon:**
  - https://www.flaticon.com/free-icon/log-out_8944313?term=logout&page=1&position=21&origin=search&related_id=8944313
- **Edit Icon:**
  - https://www.flaticon.com/free-icon/pencil_1828911?related_id=1828911
- **Help Icon:**
  - https://www.flaticon.com/free-icon/help_657093?term=help&page=1&position=59&origin=search&related_id=657093
- **Search Icon:**
  - https://www.flaticon.com/free-icon/find_9177086?term=search&page=1&position=28&origin=search&related_id=9177086
- **Clear Filter Icon:**
  - https://www.flaticon.com/free-icon/clear-filter_6159408?term=clear+filter&page=1&position=72&origin=search&related_id=6159408
- **Main Page Images:**
  - https://www.freepik.com/free-photo/businesspeople-working-office_899406.htm#query=business%20meetings&position=5&from_view=search&track=ais&uuid=1e31ccdf-5daf-4c70-8ab7-285ea460a0df
  - https://www.freepik.com/free-photo/room-interior-design_13195263.htm#query=empty%20class%20room&position=44&from_view=search&track=ais&uuid=284763e4-6bdc-4d6f-9025-7e2ac57dfaa8
  - https://www.freepik.com/free-photo/modern-office-building_1283894.htm#query=empty%20school%20building&position=21&from_view=search&track=ais&uuid=0b8357c9-b426-4284-bb45-60fb0c6ce106
- **Create School Page Background Image:**
  - https://www.freepik.com/free-photo/medium-shot-people-sitting-library_20942917.htm#query=school%20board%20meeting&position=26&from_view=search&track=ais&uuid=081fe36a-f545-4b27-bcf2-0bd5cf9d45d0
