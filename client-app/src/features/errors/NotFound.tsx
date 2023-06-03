import React from "react";
import { Link } from "react-router-dom";
import { Button, Header, Icon, Segment } from "semantic-ui-react";

const NotFound = () => {
  return (
    <Segment>
      <Header>
        <Icon name="search" />
        Oops. We coudn't find what you are looking for!
      </Header>
      <Segment.Inline>
        <Button as={Link} to={"/activities"} content="Return to activities page" />
      </Segment.Inline>
    </Segment>
  );
};

export default NotFound;
