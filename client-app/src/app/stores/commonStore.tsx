import { makeAutoObservable, reaction } from "mobx";
import { ServerError } from "../models/serverError";

export default class CommonStore {
  //setting type
  error: ServerError | null = null;
  token: string | null = localStorage.getItem('jwt');
  appLoaded = false;
//Adding a reaction which reacts to a change in the observable above, token
  constructor() {
    makeAutoObservable(this);

    reaction(
      () => this.token, token => {
        if(token){
          localStorage.setItem('jwt', token);
        }else {
          localStorage.removeItem('jwt');
        }
      }
    )
    
  }

  setServerError(error: ServerError) {
    this.error = error;
  }

  setToken = (token: string | null) => {
    this.token = token;
  };

    setAppLoaded = () => {
        this.appLoaded = true;
    }
  
}
