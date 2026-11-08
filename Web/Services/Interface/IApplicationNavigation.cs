namespace Web.Services.Interface
{
    public interface IApplicationNavigation
    {
        public void NavigateToNotFound();
        public void NavigateToUrl(string url);
        public void NavigateToFitur();
        public void NavigateToMasalah();
        public void NavigateToCaraKerja();
        public void NavigateToTestimoni();
        public void NavigateToFaq();
        public void NavigateToPage();
        public void NavigateToEvents();
        public void NavigateToChallenge(string encodeReturnUrl);
        public void NavigateToDashboard(string eventId);
        public void NavigateToParticipants(string eventId);
        public void NavigateToParticipantsCheckIn(string eventId);
        public void NavigateToFormEdit(string eventId);
        public void NavigateToFormPreview(string eventId);
        public void NavigateToFormShare(string eventId);
        public void NavigateToTicketEdit(string eventId);
        public void NavigateToTicketPreview(string eventId);
        public void NavigateToTicketPrice(string eventId);
        public void NavigateToFunds(string eventId);
    }
}
