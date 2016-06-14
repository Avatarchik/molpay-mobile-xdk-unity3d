﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using MOLPayXDK;

public class NewBehaviourScript : MonoBehaviour
{
    MOLPay molpay = new MOLPay();

    void Start()
    {
        Dictionary<String, object> paymentDetails = new Dictionary<String, object>();
        paymentDetails.Add(MOLPay.mp_amount, "1.10");
        paymentDetails.Add(MOLPay.mp_username, "molpayapi");
        paymentDetails.Add(MOLPay.mp_password, "*M0Lp4y4p1!*");
        paymentDetails.Add(MOLPay.mp_merchant_ID, "molpaymerchant");
        paymentDetails.Add(MOLPay.mp_app_name, "wilwe_makan2");
        paymentDetails.Add(MOLPay.mp_verification_key, "501c4f508cf1c3f486f4f5c820591f41");
        paymentDetails.Add(MOLPay.mp_order_ID, "XP012");
        paymentDetails.Add(MOLPay.mp_currency, "MYR");
        paymentDetails.Add(MOLPay.mp_country, "MY");
        paymentDetails.Add(MOLPay.mp_channel, "");
        paymentDetails.Add(MOLPay.mp_bill_description, "X-Platform debug");
        paymentDetails.Add(MOLPay.mp_bill_name, "Developer");
        paymentDetails.Add(MOLPay.mp_bill_email, "kheechieng.tan@gmail.com");
        paymentDetails.Add(MOLPay.mp_bill_mobile, "+1234567");
        paymentDetails.Add(MOLPay.mp_channel_editing, false);
        paymentDetails.Add(MOLPay.mp_editing_enabled, false);
        paymentDetails.Add(MOLPay.mp_is_escrow, "");

        String[] binlock = new String[] { "", "" };
        paymentDetails.Add(MOLPay.mp_bin_lock, binlock);
        paymentDetails.Add(MOLPay.mp_bin_lock_err_msg, "");
        paymentDetails.Add(MOLPay.mp_transaction_id, "");
        paymentDetails.Add(MOLPay.mp_request_type, "");

        molpay.StartMolpay(paymentDetails, MolpayCallback);
        //molpay.TransactionRequest(paymentDetails, MolpayCallback);
    }

    void Update()
    {

    }

    public void Close()
    {
        molpay.CloseMolpay();
    }

    public void MolpayCallback(string transactionResult)
    {
        Debug.Log("MolpayCallback transactionResult = " + transactionResult);
    }
}
