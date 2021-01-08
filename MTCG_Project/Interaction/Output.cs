using System;
using System.Collections.Generic;
using System.Text;

namespace MTCG_Project.Interaction
{
    static public class Output
    {
        static public string UserDoesNotExist = "User existiert nicht!";
        static public string AuthError = "Authentifizierung fehlgeschlagen/Nicht eingeloggt!";
        static public string PermissionsDenied = "Diese Aktion erfordert Adminrechte!";

        static public string UserCreated = "User wurde erfolgreich erstellt!";
        static public string UserLoginSuccess = "User wurde erfolgreich eingeloggt!";
        static public string UserAlreadyLoggedIn = "User bereits eingeloggt!";
        static public string UserdataAccessError = "Man kann auf die persönlichen Daten von anderen Usern nicht zugreifen!";
        static public string UserdataEditSuccess = "Profil wurde erfolgreich bearbeitet!";
        static public string UserAlreadyInDB = "User mit selbem Username existiert bereits!";
        static public string UserPasswordError = "Password does not fit Username!";

        static public string FriendlistSelfInteraction = "Man ist stets sein eigener bester Freund!";
        static public string FriendlistRequestSent = "Anfrage verschickt!";
        static public string FriendlistRequestAccepted = "Freundschaftsanfrage angenommen!";
        static public string AlreadyFriends = "Bereits Freunde!";
        static public string FriendlistRemoveSuccess = "Erfolgreich gelöscht!";
        static public string FriendlistRemoveError = "Freundesliste ist leer!";

        static public string PackageAddedSuccess = "Package wurde erfolgreich hinzugefügt!";
        static public string PackageAddedError = "Das gewünschte Package existiert bereits in der Datenbank!";
        static public string PackageTransactionSuccess = "Package wurde erfolgreich gekauft!";
        static public string PackageTransactionError = "Es existieren keine Packages, die gekauft werden könnten!";
        static public string InsufficientCoins = "Nicht genug Muenzen im Besitz!";

        static public string DeckEmpty = "The Deck is still empty!";
        static public string DeckUpdateSuccess = "Deck wurde erfolgreich aktualisiert!";
        static public string DeckUpdateError = "Es wurden keine passenden vier Karten ausgewählt!";

        static public string TradeCreationSuccess = "Trade Deal erfolgreich erstellt!";
        static public string TradeCreationInvalidCard = "Die angegebene Karte kann nicht getauscht werden!";
        static public string TradeCreationAlreadyExists = "Es existiert bereits ein Trading Deal mit der angegebenen Karte oder ID!";
        static public string TradeDeletionSuccess = "Deal erfolgreich gelöscht!";
        static public string TradeDeletionError = "Es kann kein Deal mit der angegebenen Karte gelöscht werden!";
        static public string TradeSuccess = "Tausch erfolgreich!";
        static public string TradeConditionsNotMet = "Tausch leider nicht erfolgreich, Anforderungen nicht erfüllt!";
        static public string TradeInvalidCard = "Karte existiert nicht oder kann nicht getauscht werden!";
        static public string TradeSelfTrade = "Man kann nicht mit sich selbst handeln!";
        
        static public string CardSoldSuccess = "Karte erfolgreich für eine Muenze verkauft!";
        static public string CardSoldError = "Die ausgewählte Karte ist nicht im Besitz oder kann nicht gewählt werden!";

        static public string RatedMatchDeckNotDefined = "Es muss zuerst ein Deck definiert werden!";
        static public string FriendMatchDeckNotDefined = "Es haben nicht beide User ein Deck definiert!";
        static public string FriendMatchNotInFl = "Nicht befreundet!";
        static public string RatedMatchGoodOpp = "Ein Gegner mit ähnlicher Elo wurde gefunden!";
        static public string RatedMatchBadOpp = "Ein Gegner wurde gefunden!";
        static public string RatedMatchNoOpp = "Es existiert keine möglicher Gegner!";
        static public void WriteConsole(string outputString)
        {
            Console.WriteLine(outputString + "\n");
        }
    }
}
