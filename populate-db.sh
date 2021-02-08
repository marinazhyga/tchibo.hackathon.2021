#!/bin/bash

curl --header "Content-Type: application/json" \
  --request POST \
  --data '  {
    "id": "601fff1e5b7e4d16d9d14f5f",
    "name": "Tom",
    "type": "Husband",
    "dateOfBirth": "1988-11-15",
    "occasions": [
      {
        "id": 6,
        "name": "Anniversary",
        "date": "2021-03-01"
      },
	  {
        "id": 1,
        "name": "Birthday",
        "date": "2021-11-15"
      }      
    ],
    "sizes": [      
      "M 48/50",
	  "43"
    ],
    "interests": [     
      "ski",     
      "traveling"     
    ],
    "customerNumber": "7584947365"
  }' \
  http://localhost:5000/api/FamilyMembers
  
curl --header "Content-Type: application/json" \
  --request POST \
  --data '  {
    "id": "601fff2a5b7e4d16d9d14f60",
    "name": "Ursula",
    "type": "Mommy",
    "dateOfBirth": "1961-02-15",
    "occasions": [
      {
        "id": 1,
        "name": "Birthday",
        "date": "2021-02-15"
      }      
    ],
    "sizes": [
      "M 40/42",
	  "38"
    ],
    "interests": [     
      "yoga",  	  
      "jewerly",
	  "backing"         
    ],
    "customerNumber": "7584947365"
  }' \
  http://localhost:5000/api/FamilyMembers
  
 
  curl --header "Content-Type: application/json" \
  --request POST \
  --data '  {
    "id": "601fff375b7e4d16d9d14f61", 
    "name": "Katarina",
    "type": "Sister",
    "dateOfBirth": "1985-08-28",
    "occasions": [
      {
        "id": 9,
        "name": "Graduation",
        "date": "2021-03-01"
      }   
    ],
    "sizes": [
		"S 36/38",
		"37"
	],
    "interests": [    
      "coffee" ,
	  "photos"	  
    ],
    "customerNumber": "7584947365"
  }'  \
  http://localhost:5000/api/FamilyMembers

    curl --header "Content-Type: application/json" \
  --request POST \
  --data ' {
   "id": "601fff375b7e4d16d9d14f66",
    "name": "Dirk",
    "type": "Brother",
    "dateOfBirth": "1988-10-01",
    "occasions": [
      {
        "id": 8,
        "name": "Housewarming",
        "date": "2021-04-15"
      }   
    ],
    "sizes": [
		"M 48/50"
	],
    "interests": [     
      "cooking",
      "garden"
	  ],
	"budget": 150,
    "customerNumber": "7584947365"
  }' \
  http://localhost:5000/api/FamilyMembers