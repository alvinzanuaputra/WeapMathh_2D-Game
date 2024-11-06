using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

[Serializable]
public class ConsumableItem {
    public string Name;
    public string Id;
    public string desc;
    public float price;
}

public class ShopScript : MonoBehaviour, IDetailedStoreListener
{
    IStoreController isc;

    [SerializeField] SaveLoader sl;
    public ConsumableItem c250, c500, c1500;

    void Start(){
        SetupBuilder();
    }

    private void SetupBuilder(){
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(c250.Id, ProductType.Consumable);
        builder.AddProduct(c500.Id, ProductType.Consumable);
        builder.AddProduct(c1500.Id, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        print("Initialized");
        isc = controller;
    }

    public void buy10button(){
        isc.InitiatePurchase(c250.Id);
    }

    public void buy100button(){
        isc.InitiatePurchase(c500.Id);
    }

    public void buy500button(){
        isc.InitiatePurchase(c1500.Id);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;

        print("Purchased: " + product.definition.id);

        if (product.definition.id == c250.Id) {
            _buyCoin250();
        }
        else if (product.definition.id == c500.Id) {
            _buyCoin500();
        }
        else if (product.definition.id == c1500.Id) {
            _buyCoin1500();
        }

        return PurchaseProcessingResult.Complete;
    }

    private void _buyCoin250(){
        int coinOwned = PlayerPrefs.GetInt("coinOwned", 0);
        coinOwned += 250;
        PlayerPrefs.SetInt("coinOwned", coinOwned);
        sl.updateCoinAmount();
    }

    private void _buyCoin500(){
        int coinOwned = PlayerPrefs.GetInt("coinOwned", 0);
        coinOwned += 500;
        PlayerPrefs.SetInt("coinOwned", coinOwned);
        sl.updateCoinAmount();
    }

    private void _buyCoin1500(){
        int coinOwned = PlayerPrefs.GetInt("coinOwned", 0);
        coinOwned += 1500;
        PlayerPrefs.SetInt("coinOwned", coinOwned);
        sl.updateCoinAmount();
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        print("initialization failure: " + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        print("initialization failure: " + error + " |||| " + message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        print("purchase failed " + product.definition.id + " |||| " + failureReason);
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        print("purchase failed " + product.definition.id + " |||| " + failureDescription);
    }
}
