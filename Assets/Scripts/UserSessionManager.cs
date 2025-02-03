using Niantic.Lightship.AR.Settings;
using UnityEngine;

public class UserSessionManager : MonoBehaviour
{
    void Start()
    {
        // Generate a unique User ID
        string userId = GenerateUniqueUserId();
        PrivacyData.SetUserId(userId);
        Debug.Log("User ID set: " + userId);
    }

    private string GenerateUniqueUserId()
    {
        // Logic to generate unique UserId
        return System.Guid.NewGuid().ToString();
    }
}