# Application Programming Interface - Language: C\# 

***Originally written by Touraj Ghaffari***


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

#### Parameters
    
#### Return Values

### rfScanNetwork

#### Parameters
    
#### Return Values

### rfScanIP

#### Parameters
    
#### Return Values

### rfOpenSocket

#### Parameters
    
#### Return Values

### rfCloseSocket

#### Parameters
    
#### Return Values

### rfChangeIPAddress

#### Parameters
    
#### Return Values

### rfResetReader

#### Parameters
    
#### Return Values

### rfResetReaderSocket

#### Parameters
    
#### Return Values
