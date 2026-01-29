using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Core;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UIElements;

public class IAPpremium : MonoBehaviour, IDetailedStoreListener
{
    [SerializeField] PopUp _popup;
    [SerializeField] PremiumActivation _premiumActivation;
    string id = "feaare_premium";
    void Awake()
    {
        SetupBuild();
        CheckAlreadyPurchased();
    }
    IStoreController storeController;
    void SetupBuild()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(id, ProductType.NonConsumable);
        UnityPurchasing.Initialize(this, builder);
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        print("initialized");
        storeController = controller;
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;
        if (product.definition.id == id)
        {
            Purchase();
        }
        return PurchaseProcessingResult.Complete;
    }
    public void PayAskPurchase() //iap button
    {
        try
        {
            if (!_premiumActivation.IsPremium)
            {
                storeController.InitiatePurchase(id);
            }
        }
        catch
        {
        }
    }
    void CheckAlreadyPurchased()
    {
        if (storeController != null)
        {
            var product = storeController.products.WithID(id);
            if (product != null)
            {
                if (product.hasReceipt)
                {
                    Purchase();
                }
            }
        }
    }
    void Purchase()
    {
        Debug.Log("purchased");
        _premiumActivation.IsPremium = true;
    }
    public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureDescription failureDescription)
    {
        _popup.ShowPopup("Connection failed");
        Debug.Log("error");
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        _popup.ShowPopup("Connection failed");
        Debug.Log("error");
    }
    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        _popup.ShowPopup("Connection failed");
        Debug.Log("error");
    }
    public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason)
    {
       _popup.ShowPopup("Connection failed");
        Debug.Log("error");
    }
}
