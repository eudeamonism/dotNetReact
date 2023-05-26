import { Button, Container, Menu } from "semantic-ui-react";
import { NavLink } from "react-router-dom";

//NavLink over Link in RRD creates a status of active which can be used to highlight UI elements
//''as'' is a prop that allows us to pass RRD NavLink where to is the actual endpoint.
export default function NavBar() {
  return (
    <Menu inverted fixed="top">
      <Container>
        <Menu.Item header as={NavLink} to={"/"}>
          <img src="/assets/logo.png" alt="logo" style={{ marginRight: "20px" }} />
          Reactivities
        </Menu.Item>
        <Menu.Item as={NavLink} to={"/activities"} name="Activities" />
        <Menu.Item>
          <Button as={NavLink} to={"/createActivity"}
            positive
            content="Create Activity"
          />
        </Menu.Item>
      </Container>
    </Menu>
  );
}

//For the onClick event, TS requires an optional id, which was throwing an error on onClick, but we wrapped it in an arrow function.
