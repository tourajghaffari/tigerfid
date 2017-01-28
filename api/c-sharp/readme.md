# Application Programming Interface - Language: C\# 

## Introduction

The API functions may execute in a synchronous or asynchronous manner. Most of the API functions will execute asynchronously. The following descriptions illustrate several possible scenarios that may result from an API function call.

1.	The function returns `RF_S_DONE` indicating that a valid response was sent either by API or the device and this is the final return value for the function call sent to API.

2.	The function returns `RF_E_XXX` indicating an error condition. The specific return value identifies the cause of the error. Any output arguments are not guaranteed to be valid.

3.	The function returns `RF_S_PEND` indicating that it will be executed asynchronously. The application should expect one or more callbacks corresponding to this function. A callback with `RF_S_OK_PEND` status indicates the function sent to API was a broadcast command and this return value is a device response to this command. There might be more responses from other devices. The last callback will have `RF_S_DONE` status indicating that it is the last callback for the function. 

4.	The function returns `RF_S_PEND` indicating that it will be executed asynchronously. A callback with `RF_S_DONE` status indicates the function that was sent to API was executed successfully and this is the last response for this function from API.

5.	The function returns `RF_S_PEND` indicating that it will be executed asynchronously. A callback with `RF_E_XXX` status indicates the function that there was an error executing this command.   

All functions will have a unique packet ID. The value of this argument will be passed to the callback function. The application can use this value to match the callback to originating function call.

## Commands

### rfOpen

The rfOpen function opens a communications channel for communicating with readers. All readers connected to the channel must support the communication parameters specified in this function. This function executes synchronously.

```
long rfOpen (
  UInt32 baudRate,   // baud rate
  UInt32 comport,    // comm. port number
  HANDLE* hConn      // connection handle
);
```

#### Parameters

`baudRate` - [in] Baud rate in bits per second.

`comPort` - [in] Communication port number.

`hConn` - [out] Connection handle. Used as input parameter to other functions. This handle is valid only if the return value indicates success.
    
#### Return Values

If successful, `RF_S_DONE`. Otherwise, error code.
    
### rfClose

The rfClose function closes a communications channel that was previously opened with the rfOpen function. This function executes synchronously.

```
long rfClose (
  HANDLE hConn	// connection handle
);
```

#### Parameters

`hConn` - [in] Connection handle returned by open function. The handle value is no longer valid and must not be used if this function returns a success value.

#### Return Values

If successful, `RF_S_DONE`. Otherwise, error code.

### rfScanNetwork

The rfScanNetwork searches the network for any reader with active network connection. 

```
long rfScanNetwork (
  UInt16 pktID
);
```

#### Parameters

`pktID` - [in] Packet identifier used to match callbacks to a specific function call.

#### Return Values

* `RF_S_PEND` if function acknowledged by API.

*	`RF_S_OK_PEND` if function acknowledged and responded successfully by each reader or tag when broadcasting, it should be followed by `RF_S_DONE` when function times out and there are no more responses from the reader, repeater, or tag.

*	`RF_S_DONE` if function successful and acknowledged by reader, repeater, or API.

Otherwise, error code.

`rfReaderEvent_t` structure in reader call back function contains information about IP address (xxx.xxx.xxx.xxx) of the reader. The table below describes which items in the `rfReaderEvent_t` structure are valid for this function.

Items | Valid
--- | ---
Host | Yes
Reader | No
repeater | No
Ip | Yes
Port | Yes
Relay | No
fGenerator | No
eventType | Yes
cmdType | Yes
eventStatus | Yes
errorStatus | Yes
pktID | Yes
cmdRef | No
Data | No
versionInfo | No

### rfScanIP

The `rfScanIP` searches the network for any reader with specific IP and OEM address. 

```
long rfScanIP (
  Byte ip[ ],
  UInt16 pktID
);
```

#### Parameters

`ip` - [in] Reader internet address in form of xxx.xxx.xxx.xxx, or a valid host name.
`pktID` - [in] Packet identifier used to match callbacks to a specific function call. Range from 1 to 223. Each consecutive API function must have different packet ID than previous one.

#### Return Values

If successful and acknowledged by reader and API, `RF_S_DONE`. Otherwise, error code.

`rfReaderEvent_t` structure in reader call back function contains information about IP address (xxx.xxx.xxx.xxx) of the reader. The table below describes which items in the `rfReaderEvent_t` structure are valid for this function.

Items | Valid
--- | ---
Host | Yes
Reader | No
repeater | No
Ip | Yes
Port | Yes
Relay | No
fGenerator | No
eventType | Yes
cmdType | Yes
eventStatus | Yes
errorStatus | Yes
pktID | Yes
cmdRef | No
Data | No
versionInfo | No

### rfOpenSocket

The `rfOpenSocket` function opens a communications channel over network for communicating with reader. This function executes synchronously.

```
long rfOpenSocket (
  Byte ip[ ],		// IP Address
  UInt16 host,		// function type
  Boolean encrypt,	// encryption
  UInt16 cmdType,	// function type
  UInt16 pktID		// packet id used in callback
);
```

#### Parameters

`ip` - [in] Reader internet address in form of  xxx.xxx.xxx.xxx, or a valid host name.

`host` - [in] Address of the host.

`encrypt` - [in] Encrypt the transmission and receive data between the host and the reader over network. If true encrypt it, false otherwise.

`cmdType` - [in] Send the command to specific enabled socket or broadcast it to all enabled sockets.

Value | Meaning
--- | ---
ALL_IPS | All enabled socketss
SPECIFIC_ID | Specific enabled socket

`pktID` - [in] Packet identifier used to match callbacks to a specific function call. Range from 1 to 223. Each consecutive API function must have different packet ID than previous one.

#### Return Values

If successful and acknowledged by reader, repeater, or API, `RF_S_DONE`. Otherwise, error code.

### rfCloseSocket

The `rfCloseSocket` function closes the communications channel already opened with `rfOpenSocket`. This function executes synchronously.
  
```
long rfCloseSocket (
  Byte ip[ ],
  UInt16 cmdType
);
```

#### Parameters

`ip` - [in] Reader internet address in form of  xxx.xxx.xxx.xxx, if cmdType is SPECIFIC_IP, otherwise NULL.

`cmdType` - [in] Send the command to specific enabled socket or broadcast it to all enabled sockets.

Value | Meaning
--- | ---
ALL_IPS | All enabled socketss
SPECIFIC_ID | Specific enabled socket

#### Return Values

If successful and acknowledged by reader, repeater, or API, `RF_S_DONE`. Otherwise, error code.

### rfChangeIPAddress

The `rfChangeIPAddress` function changes network IP address of a reader to a new IP address. This function executes synchronously.

```
long rfChangeIPAddress (
  Byte oldIP[],	// Old IP Address
  Byte newIP[]	// New IP Address
);
```

#### Parameters

`oldIP` - [in] Reader network address in form of  xxx.xxx.xxx.xxx

`newIP` - [in] Reader new network address in form of  xxx.xxx.xxx.xxx

#### Return Values

If successful and acknowledged by reader, repeater, or API, `RF_S_DONE`. Otherwise, error code.

### rfResetReader

The `rfResetReader` function requests a reader to perform a reset sequence. The API will call the registered `rfReaderEvent` callback function when the reader is back on line.

```
long rfResetReader (
  UInt16 host		// address of reader
  UInt16 reader		// address of host
  UInt16 repeater	// address of repeater
  UInt16 cmdType		// command type
  UInt16 pktID		// packet ID
);
```

#### Parameters

`host` - [in] Address of the host.
`reader` - [in] Address of the reader
`repeater` - [in] Address of the repeater.
`cmdType` - [in] Send the command to specific reader or broadcast it to all readers.
                           
Value | Meaning
--- | ---
ALL_READERS | All readers in the system
ALL_REPEATERS | All repeaters in the system
SPECIFIC_READER | Specified reader address
SPECIFIC_REPEATER | Specified repeater address

`pktID` - [in] Packet identifier used to match callbacks to a specific function call. Range from 1 to 223. Each consecutive API function must have different packet ID than previous one.

#### Return Values

* `RF_S_PEND` if function acknowledged by API

* `RF_S_OK_PEND` if function acknolwedged and responded successfully by each reader or tag when broadcasting. It should be followed by `RF_S_DONE` when function times out and there are no more responses from the reader, repeater, or tag.

* `RF_S_DONE` if function successful and acknowledged by reader, repeater, or API.

Otherwise, error code.

`rfReaderEvent_t` structure in reader call back function contains information about reader being reset. The table below describes which items in the `rfReaderEvent_t` structure are valid for this function.

**cmdType:** ALL_READERS, ALL_REPEATERS

Items | RF_S_OK_PEND | RF_S_DONE | RF_E_XXX
--- | --- | --- | ---
host | Yes | No | Yes
reader | Yes | No | Yes
repeater | Yes | No | Yes
relay | No | No | No
fGenerator | No | No | No
eventType | Yes | Yes | Yes
cmdType | Yes | Yes | Yes
eventStatus | Yes | Yes | Yes
errorStatus | No | No | Yes
pktID | Yes | Yes | Yes
cmdRef | No | No | No
data | No | No | No
versionInfo | No | No | No

**cmdType:** SPECIFIC_READERS, SPECIFIC_REPEATERS

Items | RF_S_DONE | RF_E_XXX
--- | --- | ---
host | Yes | Yes
reader | Yes | Yes
repeater | Yes | Yes
relay | No | No
fGenerator | No | No
eventType | Yes | Yes
cmdType | Yes | Yes
eventStatus | Yes | Yes
errorStatus | No | Yes
pktID | Yes | Yes
cmdRef | No | No
data | No | No
versionInfo | No | No

### rfResetReaderSocket

The `rfResetReaderSocket` function requests a reader with specific IP address to perform a reset sequence. The API will call the registered `rfReaderEvent` callback function when the reader is back on line.

```
long rfResetReaderSocket (
  UInt16 host,
  Byte ip[],
  UInt16 pktID
);
```

#### Parameters

`host` - [in] Address of the host.

`ip` - [in] Reader internet address in form of xxx.xxx.xxx.xxx, or a valid host name.

`pktID` - [in] Packet identifier used to match callbacks to a specific function call. Range from 1 to 223. Each consecutive API function must have different packet ID than previous one.

#### Return Values

* `RF_S_PEND` if function acknowledged by API

* `RF_S_OK_PEND` if function acknolwedged and responded successfully by each reader or tag when broadcasting. It should be followed by `RF_S_DONE` when function times out and there are no more responses from the reader, repeater, or tag.

* `RF_S_DONE` if function successful and acknowledged by reader, repeater, or API.

Otherwise, error code.

`rfReaderEvent_t` structure in reader call back function contains information about reader being reset. The table below describes which items in the `rfReaderEvent_t` structure are valid for this function.

**cmdType:** ALL_READERS, ALL_REPEATERS

Items | RF_S_OK_PEND | RF_S_DONE | RF_E_XXX
--- | --- | --- | ---
host | Yes | No | Yes
reader | Yes | No | Yes
repeater | Yes | No | Yes
relay | No | No | No
fGenerator | No | No | No
eventType | Yes | Yes | Yes
cmdType | Yes | Yes | Yes
eventStatus | Yes | Yes | Yes
errorStatus | No | No | Yes
pktID | Yes | Yes | Yes
cmdRef | No | No | No
data | No | No | No
versionInfo | No | No | No

**cmdType:** SPECIFIC_READERS, SPECIFIC_REPEATERS

Items | RF_S_DONE | RF_E_XXX
--- | --- | ---
host | Yes | Yes
reader | Yes | Yes
repeater | Yes | Yes
relay | No | No
fGenerator | No | No
eventType | Yes | Yes
cmdType | Yes | Yes
eventStatus | Yes | Yes
errorStatus | No | Yes
pktID | Yes | Yes
cmdRef | No | No
data | No | No
versionInfo | No | No
