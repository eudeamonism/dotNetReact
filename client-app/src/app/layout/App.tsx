import { Container } from "semantic-ui-react";
import NavBar from "./NavBar";
import { observer } from "mobx-react-lite";
import { Outlet, useLocation } from "react-router-dom";
import HomePage from "../../features/home/HomePage";

//Going to setup an interface for App.tsx?

function App() {
  //We want to the homepage to be without a navbar. Therefore we removed it from our routes since it always prints to the Outlet which has a fixed nav.
  const location = useLocation();
//useLocation contains a method 'pathname' which we can specify and output homepage else everything else.
  return (
    <>
      {location.pathname === "/" ? (
        <HomePage />
      ) : (
        <>
          <NavBar />
          <Container style={{ marginTop: "7em" }}>
            <Outlet />
          </Container>
        </>
      )}
    </>
  );
}

//Import observer and wrap App as its parameter
//The outlet is a placeholder for react-router-dom to load the page which the link is referring.
export default observer(App);
