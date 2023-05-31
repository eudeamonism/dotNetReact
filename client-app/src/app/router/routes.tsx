import { RouteObject, createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import ActivityForm from "../../features/activities/form/ActivityForm";
import ActivityDetail from "../../features/activities/details/ActivitiyDetails";

//Objectify routes to make code cleaner, notice RouteObject import
export const routes: RouteObject[] = [
  {
    //root route aka Top of Tree
    path: "/",
    element: <App />,
    children: [
      { path: "activities", element: <ActivityDashboard /> },
      { path: "activities/:id", element: <ActivityDetail /> },
      { path: "createActivity", element: <ActivityForm key='create' /> },
      { path: "manage/:id", element: <ActivityForm key='manage'/> },
    ],
  },
];

//We need to access root, which is the index.tsx file
//We added a key to createActivity and manage/:id because they have same state given element, therefore key differentiates them.
export const router = createBrowserRouter(routes);
