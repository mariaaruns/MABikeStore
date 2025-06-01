using BikeStore.Domain.DTO.Response.LookupResponse;

namespace BikeStore.UI.Services
{
    public class GlobalStoreFilterService
    {

        public List<GetDropdownResponse> Stores { get; } = new()
    {
        new GetDropdownResponse { Value = 1, Text = "Main Store" },
        new GetDropdownResponse { Value = 2, Text = "Branch Store" },
        new GetDropdownResponse { Value = 3, Text = "Outlet Store" },
    };
        public event Action OnChange;
        public GetDropdownResponse? Store { get; set; } =new();

        public void SetStore(GetDropdownResponse storeId)
        {
            Store = storeId;
            NotifyStateChanged();
        }

        public void NotifyStateChanged() => OnChange?.Invoke();


        

    }
}
