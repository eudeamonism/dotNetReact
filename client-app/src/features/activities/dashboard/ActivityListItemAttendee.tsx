import { observer } from "mobx-react-lite";
import { List, ListItem, Image, Popup, Item } from "semantic-ui-react";
import { Profile } from "../../../app/models/profile";
import { Link } from "react-router-dom";
import ProfileCard from "../../profiles/ProfileCard";

//Adding interface is always outside the component

interface Props {
  attendees: Profile[];
}

export default observer(function ActivityListItemAttendee({ attendees }: Props) {
  return (
    <List horizontal>
      {attendees.map((attendee) => (
        <Popup
          hoverable
          key={attendee.username}
          trigger={
            <List.Item style={{marginBottom: 3}} key={attendee.username} as={Link} to={`/profile/${attendee.username}`}>
              <Item.Image circular size="mini" src={attendee.image || "/assets/user.png"} />
            </List.Item>
          }
        >
          <Popup.Content>
            <ProfileCard profile={attendee} />
          </Popup.Content>
        </Popup>
      ))}
    </List>
  );
});
