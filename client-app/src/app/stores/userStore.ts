import { makeAutoObservable, runInAction } from "mobx";
import { User, UserFormValues } from "../models/user";
import agent from "../api/agent";
import { store } from "./store";
import { router } from "../router/routes";
//Making another store, and this time it is for user.

//we utilize the class object to initialize our store
export default class UserStore {
  user: User | null = null;

  constructor() {
    //this method is from MobX
    makeAutoObservable(this);
  }

  //Now these are our methods
  //the !! operator is a boolean operator, returning true of false
  // get is a getter function that is called when item is accessed.
  get isLoggedIn() {
    return !!this.user;
  }

  //ACTION FUNCTION
  //This method takes a param which we import ts types
  login = async (creds: UserFormValues) => {
    try {
      //We are using agent import which has method of Account.login where creds are our properties / payload
      const user = await agent.Account.login(creds);
      //We are setting our token in our store
      store.commonStore.setToken(user.token);
      //Because we await.
      runInAction(() => (this.user = user));
      router.navigate("/activities");
      store.modalStore.closeModal();
    } catch (error) {
      throw error;
    }
  };
  
  register = async (creds: UserFormValues) => {
    try {
      //We are using agent import which has method of Account.login where creds are our properties / payload
      const user = await agent.Account.register(creds);
      //We are setting our token in our store
      store.commonStore.setToken(user.token);
      //Because we await.
      runInAction(() => (this.user = user));
      router.navigate("/activities");
      store.modalStore.closeModal();
    } catch (error) {
      throw error;
    }
  };


  logout = () => {
    store.commonStore.setToken(null);
    this.user = null;
    router.navigate("/");
  };

  //ACTION FUNCTION
  getUser = async () => {
    try {
      const user = await agent.Account.current();
      runInAction(() => (this.user = user));
    } catch (error) {
      console.log(error);
    }
  };
}
