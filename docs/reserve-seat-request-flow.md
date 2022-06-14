# Reserve a seat at a movie (request flow)
Using the movie id one can get a performance id via a simple get request.

To reserve a seat, the performance id is needed.

## 1. Requests the occupation for the performance
GET https://www.vuecinemas.nl/services/api/cart.php

### PAYLOAD
```
action:occupation
performance_id:000000000
```
### RESPONSE
```json
{
    "success": true,
    "data": [],
    "message": ""
}
```
### NOTES
The numbers in the data property represents which seats are occupied.
<li> How do we know how many seats are available in this performance? (rows, columns, etc.)
<li> how do we know which of the seats are optimal for viewing (or are vip)?


## 2. Reserve a seat (reserve)
GET https://www.vuecinemas.nl/services/api/cart.php

### PAYLOAD
```
action:reserve
performance_id:000000000
reservation[performanceId]:000000000
reservation[seat][number]:34
reservation[seat][row]:3
reservation[seat][seat]:5
reservation[seat][area]:1
reservation[seat][type]:1
reservation[priceCategory][id]:2189390841
reservation[priceCategory][performance_id]:000000000
reservation[priceCategory][price_oid]:10000000999YBADPUC
reservation[priceCategory][price_name]:Standaard+.
reservation[priceCategory][price]:12.5
reservation[priceCategory][reservation_fee]:1.00
reservation[priceCategory][ticket_fee]:0.00
reservation[priceCategory][allowed_for]:0
reservation[priceCategory][minimum_quantity]:1
reservation[priceCategory][maximum_quantity]:0
reservation[priceCategory][ticket_type_oid]:
reservation[priceCategory][seating_area_oid]:1
reservation[priceCategory][_price]:€+12.50
reservation[priceCategory][type]:REGULAR
```

### RESPONSE
```json
{
    "success": true,
    "data": {
        "reservations": [
            {
                "id": "<GUID>",
                "price": null,
                "performanceId": 000000000,
                "seat": {
                    "number": 34,
                    "row": 3,
                    "seat": 5,
                    "area": 1,
                    "type": 1
                },
                "priceCategory": {
                    "id": 2189390841,
                    "type": "REGULAR",
                    "name": null,
                    "discount": null,
                    "seating_area_oid": 1,
                    "price": 12.5,
                    "price_oid": "10000000999YBADPUC",
                    "isVerified": null,
                    "code": "",
                    "price_name": "Standaard ."
                }
            }
        ],
        "expiresInSeconds": 725,
        "expirationTimestamp": 1655235837
    },
    "message": null
}
```

## 3. Confirmation of reserved seat.
GET: https://www.vuecinemas.nl/services/api/cart.php

### PAYLOAD
```
action:occupation
performance_id:110755
```

### RESPONSE
```json
{ 
    "success": true,
    "data": [34],
    "message":""
}
```

## 4. Select Vue Movie Pass (update_reservation)
GET https://www.vuecinemas.nl/services/api/cart.php

### PAYLOAD
```
action:update_reservation
performance_id:000000000
reservation[id]:<GUID>
reservation[performanceId]:000000000
reservation[priceCategory][id]:999999991
reservation[priceCategory][type]:CUSTOMERCARD
reservation[priceCategory][seating_area_oid]:1
reservation[priceCategory][price_oid]:10000000999YBADPUC
reservation[priceCategory][price_name]:Vue+Movie+Pass
reservation[priceCategory][price]:
reservation[priceCategory][_info]:Tickets+voor+houders+van+de+Movie+Pass.+Houd+het+10+cijferige+pasnummer+en+4+cijferige+pincode+bij+de+hand+om+in+stap+2+van+het+bestelproces+in+te+vullen.
reservation[priceCategory][_price]:
reservation[seat][number]:34
reservation[seat][row]:3
reservation[seat][seat]:5
reservation[seat][area]:1
reservation[seat][type]:1
reservation[price]:
```

## RESPONSE
```json
{
    "success": true,
    "data": {
        "id": "<GUID>",
        "price": null,
        "performanceId": 000000000,
        "seat": {
            "number": 34,
            "row": 3,
            "seat": 5,
            "area": 1,
            "type": 1
        },
        "priceCategory": {
            "photo": null,
            "name": null,
            "user_id": null,
            "code": "",
            "pin": null,
            "id": 999999991,
            "type": "CUSTOMERCARD",
            "discount": null,
            "seating_area_oid": 1,
            "price": false,
            "price_oid": "10000000999YBADPUC",
            "isVerified": null
        },
        "isManualSelection": false
    },
    "message": null
}
```

## 5. Enter Movie Pass Details
GET https://www.vuecinemas.nl/services/api/cart.php

### PAYLOAD
```
action:verify
performance_id:000000000
reservation[id]:<GUID>
reservation[performanceId]:000000000
reservation[priceCategory][id]:999999991
reservation[priceCategory][type]:CUSTOMERCARD
reservation[priceCategory][seating_area_oid]:1
reservation[priceCategory][price_oid]:10000000999YBADPUC
reservation[priceCategory][price_name]:Vue+Movie+Pass
reservation[priceCategory][price]:
reservation[priceCategory][_info]:Tickets+voor+houders+van+de+Movie+Pass.+Houd+het+10+cijferige+pasnummer+en+4+cijferige+pincode+bij+de+hand+om+in+stap+2+van+het+bestelproces+in+te+vullen.
reservation[priceCategory][_price]:
reservation[seat][number]:34
reservation[seat][row]:3
reservation[seat][seat]:5
reservation[seat][area]:1
reservation[seat][type]:1
reservation[price]:
priceCategory[code]:<CODE>
priceCategory[pin]:<PIN>
priceCategory[isCodeOk]:true
priceCategory[isPinOk]:true
priceCategory[canBeVerified]:true
priceCategory[validationError]:
priceCategory[maxLengthCode]:11
priceCategory[maxLengthPin]:4
priceCategory[id]:999999991
priceCategory[type]:CUSTOMERCARD
priceCategory[seating_area_oid]:1
priceCategory[price_oid]:10000000999YBADPUC
priceCategory[price_name]:Vue+Movie+Pass
priceCategory[price]:
priceCategory[_info]:Tickets+voor+houders+van+de+Movie+Pass.+Houd+het+10+cijferige+pasnummer+en+4+cijferige+pincode+bij+de+hand+om+in+stap+2+van+het+bestelproces+in+te+vullen.
priceCategory[_price]:
priceCategory[isVerified]:false
```

### RESPONSE
```json
{
    "success": true,
    "data": {
        "id": "<GUID>",
        "price": 0,
        "performanceId": 000000000,
        "seat": {
            "number": 34,
            "row": 3,
            "seat": 5,
            "area": 1,
            "type": 1
        },
        "priceCategory": {
            "photo": "userfiles/image/card_photos/<GUID>.jpg",
            "name": "<NAME>",
            "user_id": 00000000,
            "code": 000000000,
            "pin": 0000,
            "id": 999999991,
            "type": "CUSTOMERCARD",
            "discount": null,
            "seating_area_oid": 1,
            "price": false,
            "price_oid": "10000000999YBADPUC",
            "isVerified": true
        },
        "isManualSelection": false
    },
    "message": null
}
```

## 6. Update step (login -> payment?)
POST: https://www.vuecinemas.nl/services/api/cart.php

### PAYLOAD
```
action: update
attributes[steps][0][id]: seating
attributes[steps][0][action]: show:step:seating
attributes[steps][0][name]: Stoelkeuze
attributes[steps][0][navHTML]: 1<span>. Stoelkeuze</span>
attributes[steps][0][active]: false
attributes[steps][0][enabled]: true
attributes[steps][1][id]: summary
attributes[steps][1][action]: show:step:summary
attributes[steps][1][name]: Tickets
attributes[steps][1][navHTML]: 2<span>. Tickets</span>
attributes[steps][1][enabled]: true
attributes[steps][1][active]: false
attributes[steps][2][id]: login
attributes[steps][2][action]: show:step:login
attributes[steps][2][name]: Gegevens
attributes[steps][2][navHTML]: 3<span>. Gegevens</span>
attributes[steps][2][enabled]: true
attributes[steps][2][active]: true
attributes[steps][3][id]: payment
attributes[steps][3][action]: show:step:payment
attributes[steps][3][name]: Afrekenen
attributes[steps][3][navHTML]: 4<span>. Afrekenen</span>
attributes[steps][3][enabled]: 18869591
```

### RESPONSE
```json
{
    "success": true,
    "data": {
        "steps": [
            {
                "id": "seating",
                "action": "show:step:seating",
                "name": "Stoelkeuze",
                "active": false,
                "enabled": true
            },
            {
                "id": "summary",
                "action": "show:step:summary",
                "name": "Tickets",
                "active": false,
                "enabled": true
            },
            {
                "id": "login",
                "action": "show:step:login",
                "name": "Gegevens",
                "active": true,
                "enabled": true
            },
            {
                "id": "payment",
                "action": "show:step:payment",
                "name": "Afrekenen",
                "active": null,
                "enabled": false
            }
        ]
    },
    "message": null
}
```

## 8. Next step (payment -> confirmation?)
POST: https://www.vuecinemas.nl/services/api/cart.php

### PAYLOAD
```
action: update
attributes[steps][0][id]: seating
attributes[steps][0][action]: show:step:seating
attributes[steps][0][name]: Stoelkeuze
attributes[steps][0][navHTML]: 1<span>. Stoelkeuze</span>
attributes[steps][0][active]: false
attributes[steps][0][enabled]: true
attributes[steps][1][id]: summary
attributes[steps][1][action]: show:step:summary
attributes[steps][1][name]: Tickets
attributes[steps][1][navHTML]: 2<span>. Tickets</span>
attributes[steps][1][enabled]: true
attributes[steps][1][active]: false
attributes[steps][2][id]: login
attributes[steps][2][action]: show:step:login
attributes[steps][2][name]: Gegevens
attributes[steps][2][navHTML]: 3<span>. Gegevens</span>
attributes[steps][2][enabled]: true
attributes[steps][2][active]: false
attributes[steps][3][id]: payment
attributes[steps][3][action]: show:step:payment
attributes[steps][3][name]: Afrekenen
attributes[steps][3][navHTML]: 4<span>. Afrekenen</span>
attributes[steps][3][enabled]: true
attributes[steps][3][active]: true
```

## RESPONSE
```json
{
    "success": true,
    "data": {
        "steps": [
            {
                "id": "seating",
                "action": "show:step:seating",
                "name": "Stoelkeuze",
                "active": false,
                "enabled": true
            },
            {
                "id": "summary",
                "action": "show:step:summary",
                "name": "Tickets",
                "active": false,
                "enabled": true
            },
            {
                "id": "login",
                "action": "show:step:login",
                "name": "Gegevens",
                "active": false,
                "enabled": true
            },
            {
                "id": "payment",
                "action": "show:step:payment",
                "name": "Afrekenen",
                "active": true,
                "enabled": true
            }
        ]
    },
    "message": null
}
```

## 9. Payment
GET: https://www.vuecinemas.nl/services/api/cart.php

### PAYLOAD
```
action: payment
performance_id: 000000000
payment_options[service]: 
```

### RESPONSE
```
EMPTY?
```

## 10. Confirm restrictions
GET: https://www.vuecinemas.nl/services/api/cart.php

### PAYLOAD
```
action:update
performance_id:000000000
attributes[isRestrictionsConfirmed]: 
```

## 11. Empty call? (maybe clear back-end?)
GET: https://www.vuecinemas.nl/services/api/cart.php

### PAYLOAD
```
action: empty
```

### RESPONSE
```
EMPTY?
```
