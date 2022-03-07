using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class MonetizationManager : MonoBehaviour,IUnityAdsListener
{
    internal static MonetizationManager inst;
    public bool isshow = false;
    [SerializeField] public bool aparecendoAnuncio = false,comprouNoAds = false;
    public delegate void FinishAds();
    public event FinishAds finishAds;
    private void Awake()
    {
        if (inst == null)
        {
            inst = this;
        }
        else if (inst != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        string gameId = "4231160";
        
        Advertisement.Initialize(gameId,Debug.isDebugBuild);
        if (PlayerPrefs.GetInt("comprouNoAds", 0) == 0)
        {
            comprouNoAds = false;
            
        }
        else
        {
            comprouNoAds = true;
            FindObjectOfType<IAPButton>().GetComponent<Button>().interactable = false;
        }
    }

    
    public bool isS()
    {
        return Advertisement.isShowing;
    }
    public void iaiaiai()
    {
        if (Advertisement.IsReady("Interstitial_Android") && !comprouNoAds)
        {
            Advertisement.Show("Interstitial_Android");
            Time.timeScale = 1;
            aparecendoAnuncio = true;
        }
        else
        {
            Time.timeScale = 1;
            aparecendoAnuncio = false;
        }
       
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.inst.podeJogar = true;
    }
    // Update is called once per frame
    
    void FixedUpdate()
    {
        if (Advertisement.isShowing)
        {
            Debug.Log("olaaa");
            aparecendoAnuncio = true;
            isshow = true;
        }else if(!Advertisement.isShowing && isshow)
        {
            Debug.Log("olaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            finishAds?.Invoke();
            isshow = false;
        }
    }
    public void onPurchaseComplete(Product product)
    {
        if (product.definition.id.Equals("RemoveAds"))
        {
            Debug.Log("oi");
            PlayerPrefs.SetInt("comprouNoAds", 1);
            PlayerPrefs.Save();
            comprouNoAds = true;
        }
        
    }

    public void OnUnityAdsReady(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        throw new System.NotImplementedException();
    }
}
