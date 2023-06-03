import { createContext, useContext } from "react";
import ActivityStore from "./activityStore";
import CommonStore from "./commonStore";

interface Store {
  activityStore: ActivityStore;
  commonStore: CommonStore;
}
//Add new stores
export const store: Store = {
  activityStore: new ActivityStore(),
  commonStore: new CommonStore()
};

//export store as StoreContext to be available in all components
//We are going to install like a provider in our index.html
export const StoreContext = createContext(store);

export function useStore(){
return useContext(StoreContext)
}


