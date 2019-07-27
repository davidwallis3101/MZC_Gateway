# MZC_Gateway

This is an image that contains an asp.net core web api project that allows loxone to control an MZC-66 amp (and slave if needed) via a local serial port.

I am hosting this within a docker container on my MusicServer4Lox (http://music-server.net)

this can be run via:

docker run -d --name mzc_gateway --device=/dev/ttyUSB0 -p 5000:5000 --restart on-failure davidwallis3101/mzc_gateway

appsettings.json needs work to get this onto persistent storage.

Control is via:

http://ipAddress:5000/api/poweron/1 (where 1 is the zone number)..
http://ipAddress:5000/api/poweroff/1 

there are also methods for: poweroff (power on also sets the source)

Zone numbers start at 1, not 0 as per the amp's api - this was to make things neater in my logic within loxone, I may make this configurable for the start zone.