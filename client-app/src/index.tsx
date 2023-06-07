import ReactDOM from "react-dom/client";
import "semantic-ui-css/semantic.min.css";
//Add CSS for CALENDAR
import "react-calendar/dist/Calendar.css";
import "react-toastify/dist/ReactToastify.min.css";
import 'react-datepicker/dist/react-datepicker.css'
import "./app/layout/styles.css";
import reportWebVitals from "./reportWebVitals";
import { StoreContext, store } from "./app/stores/store";
import { RouterProvider } from "react-router-dom";
import { router } from "./app/router/routes";


//Basically, MobX is hooked up utilizing React Context, useContext, and Provider
//the store is a variable that creates instances of each store
//StoreContext is a variable that utilizes Reacthook createContext(store)
//Lastly, we export a function not used here but to access store.
const root = ReactDOM.createRoot(document.getElementById("root") as HTMLElement);
root.render(
  <StoreContext.Provider value={store}>
    <RouterProvider router={router} />
  </StoreContext.Provider>
);

//We swapped App.jsx for RouterProvider to connect RRouterDOm
//It takes a router property which we set as our router variable

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
