<!--
# license: Copyright © 2011-2016 MOLPay Sdn Bhd. All Rights Reserved. 
-->

# molpay-mobile-xdk-unity3d

This is the complete and functional MOLPay Unity payment module that is ready to be implemented into Unity project through Unity C# script copy and paste. An example application project (MOLPayXDKExample) is provided for MOLPayXDK Unity integration reference.

This plugin provides an integrated MOLPay payment module that contains a wrapper 'MOLPay.cs' and an upgradable core as the 'molpay-mobile-xdk-www' folder, which the latter can be separately downloaded at https://github.com/MOLPay/molpay-mobile-xdk-www and update the local version.

## Recommended configurations

    - Unity Version: 5.3.4f1 ++

    - UniWebView 2 ++

    - Minimum Android target version: Android 4.4

    - Minimum iOS target version: 7.0
    
## MOLPay Android Caveats

    Credit card payment channel is not available in Android 4.1, 4.2, and 4.3. due to lack of latest security (TLS 1.2) support on these Android platforms natively.

## Installation

    Step 1 - Copy and paste MOLPay.cs into the Assets folder of your Unity project

    Step 2 - Copy and paste MiniJSON.cs (can be separately downloaded at https://gist.github.com/darktable/1411710#file-minijson-cs) into the Assets folder of your Unity project

    Step 3 - Copy and paste molpay-mobile-xdk-www folder (can be separately downloaded at https://github.com/MOLPay/molpay-mobile-xdk-www) into Assets\StreamingAssets\ folder of your Unity project

    Step 4 - Copy and paste custom.css into Assets\StreamingAssets\ folder of your Unity project

    Step 5 - Purchase UniWebView from http://uniwebview.onevcat.com/. After purchasing you should be able to download a Unity package file (e.g. uniwebview_2_7_1.unitypackage). Double click on the file to import it into your unity project

    Step 6 - Add the result callback function
    public void MolpayCallback (string transactionResult)
    {
        Debug.Log("MolpayCallback transactionResult = " + transactionResult);
    }
    
    =========================================
    Sample transaction result in JSON string:
    =========================================
    
    {"status_code":"11","amount":"1.01","chksum":"34a9ec11a5b79f31a15176ffbcac76cd","pInstruction":0,"msgType":"C6","paydate":1459240430,"order_id":"3q3rux7dj","err_desc":"","channel":"Credit","app_code":"439187","txn_ID":"6936766"}
    
    Parameter and meaning:
    
    "status_code" - "00" for Success, "11" for Failed, "22" for *Pending. 
    (*Pending status only applicable to cash channels only)
    "amount" - The transaction amount
    "paydate" - The transaction date
    "order_id" - The transaction order id
    "channel" - The transaction channel description
    "txn_ID" - The transaction id generated by MOLPay
    
    * Notes: You may ignore other parameters and values not stated above
    
    =====================================
    * Sample error result in JSON string:
    =====================================
    
    {"Error":"Communication Error"}
    
    Parameter and meaning:
    
    "Communication Error" - Error starting a payment process due to several possible reasons, please contact MOLPay support should the error persists.
    1) Internet not available
    2) API credentials (username, password, merchant id, verify key)
    3) MOLPay server offline.

## Using MOLPayXDK namespace

    using MOLPayXDK;

## Instantiate the Molpay object

    MOLPay molpay = new MOLPay();

## Prepare the Payment detail object

    Dictionary<String, object> paymentDetails = new Dictionary<String, object>();
    // Mandatory String. A value more than '1.00'
    paymentDetails.Add(MOLPay.mp_amount, "");

    // Mandatory String. Values obtained from MOLPay
    paymentDetails.Add(MOLPay.mp_username, "");
    paymentDetails.Add(MOLPay.mp_password, "");
    paymentDetails.Add(MOLPay.mp_merchant_ID, "");
    paymentDetails.Add(MOLPay.mp_app_name, "");
    paymentDetails.Add(MOLPay.mp_verification_key, "");

    // Mandatory String. Payment values
    paymentDetails.Add(MOLPay.mp_order_ID, "");
    paymentDetails.Add(MOLPay.mp_currency, "MYR");
    paymentDetails.Add(MOLPay.mp_country, "MY");
    
    // Optional String.
    paymentDetails.Add(MOLPay.mp_channel, ""); // Use 'multi' for all available channels option. For individual channel seletion, please refer to "Channel Parameter" in "Channel Lists" in the MOLPay API Spec for Merchant pdf. 
    paymentDetails.Add(MOLPay.mp_bill_description, "");
    paymentDetails.Add(MOLPay.mp_bill_name, "");
    paymentDetails.Add(MOLPay.mp_bill_email, "");
    paymentDetails.Add(MOLPay.mp_bill_mobile, "");
    paymentDetails.Add(MOLPay.mp_channel_editing, false); // Option to allow channel selection.
    paymentDetails.Add(MOLPay.mp_editing_enabled, false); // Option to allow billing information editing.

    // Optional for Escrow
    paymentDetails.Add(MOLPay.mp_is_escrow, ""); // Optional for Escrow, put "1" to enable escrow

    // Optional for credit card BIN restrictions
    String[] binlock = new String[] { "", "" };
    paymentDetails.Add(MOLPay.mp_bin_lock, binlock); // Optional for credit card BIN restrictions
    paymentDetails.Add(MOLPay.mp_bin_lock_err_msg, ""); // Optional for credit card BIN restrictions

    // For transaction request use only, do not use this on payment process
    paymentDetails.Add(MOLPay.mp_transaction_id, ""); // Optional, provide a valid cash channel transaction id here will display a payment instruction screen.
    paymentDetails.Add(MOLPay.mp_request_type, ""); // Optional, set 'Status' when performing a transactionRequest

    // Optional for customizing MOLPay UI
    paymentDetails.Add(MOLPay.mp_custom_css_url, "file:///android_asset/custom.css");

    // Optional, set the token id to nominate a preferred token as the default selection, set "new" to allow new card only
    paymentDetails.Add(MOLPay.mp_preferred_token, "");

    // Optional, credit card transaction type, set "AUTH" to authorize the transaction
    paymentDetails.Add(MOLPay.mp_tcctype, "");

    // Optional, set true to process this transaction through the recurring api, please refer the MOLPay Recurring API pdf
    paymentDetails.Add(MOLPay.mp_is_recurring, false);
    
## Start the payment module UI

    molpay.StartMolpay(paymentDetails, MolpayCallback);

## Close the payment module UI

    molpay.CloseMolpay();

    * Notes: The close event will also return a final result.
    
## Cash channel payment process (How does it work?)

    This is how the cash channels work on XDK:
    
    1) The user initiate a cash payment, upon completed, the XDK will pause at the “Payment instruction” screen, the results would return a pending status.
    
    2) The user can then click on “Close” to exit the MOLPay XDK aka the payment screen.
    
    3) When later in time, the user would arrive at say 7-Eleven to make the payment, the host app then can call the XDK again to display the “Payment Instruction” again, then it has to pass in all the payment details like it will for the standard payment process, only this time, the host app will have to also pass in an extra value in the payment details, it’s the “mp_transaction_id”, the value has to be the same transaction returned in the results from the XDK earlier during the completion of the transaction. If the transaction id provided is accurate, the XDK will instead show the “Payment Instruction" in place of the standard payment screen.
    
    4) After the user done the paying at the 7-Eleven counter, they can close and exit MOLPay XDK by clicking the “Close” button again.

## Support

Submit issue to this repository or email to our support@molpay.com

Merchant Technical Support / Customer Care : support@molpay.com<br>
Sales/Reseller Enquiry : sales@molpay.com<br>
Marketing Campaign : marketing@molpay.com<br>
Channel/Partner Enquiry : channel@molpay.com<br>
Media Contact : media@molpay.com<br>
R&D and Tech-related Suggestion : technical@molpay.com<br>
Abuse Reporting : abuse@molpay.com
