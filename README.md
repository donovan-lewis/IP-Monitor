<p align="center">
  <img src="https://imgur.com/wqmXlDv.png">
</p>

# IP-Monitor
Allows you to monitor the up status of machines on your network using a csv file of IP addresses

# CSV FILE FORMAT
CSV files used with this app should be formatted as a single line of IP addresses or web addresses separated by commas. If you put them on separate lines they will be ignored I only look at the first line of the file. EX: 192.168.20.10,192.168.20.11,192.168.20.12,192.168.20.13,192.168.20.14,192.168.20.15

# HOW IT WORKS
After selecting the csv file you'd like to use the application will read that first line and every 15 seconds (roughly) the application will ping the ips sequentially with a timeout of 1 second and should they stop responding the UI element associated with the given address will change from green to red and if it comes back (Ex a machine rebooted) the UI element will change back to green. All of these events get logged to the text box on the right.

# Current Issues and Future Updates
The image on the left is always a raspberry pi this will be changed in the furture to allow more variaty 
The text for the URL or IP are always lower of center this is because I am in the process of adding ability for the user to add custom names so it will be easier to parse and read the data at a glance.
There is no saved state at the moment 
