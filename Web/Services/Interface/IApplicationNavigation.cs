namespace Web.Services.Interface
{
    public interface IApplicationNavigation
    {
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
        public void NavigateToParticipants();
        public void NavigateToParticipantsCheckIn();
        public void NavigateToFormEdit();
        public void NavigateToFormPreview();
        public void NavigateToFormShare();
        public void NavigateToTicketEdit();
        public void NavigateToTicketPreview();
        public void NavigateToTicketPrice();
        public void NavigateToFunds();
    }
}
