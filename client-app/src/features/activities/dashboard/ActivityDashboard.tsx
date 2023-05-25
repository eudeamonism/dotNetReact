import { Grid } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";
import ActivityList from "./ActivityList";
import ActivityDetail from "../details/ActivitiyDetails";
import ActivityForm from "../form/ActivityForm";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";

//activities is from useState coming from App.tsx. We need to provide it an interface with our model.
interface Props {
  activities: Activity[];

  submitting: boolean;

  deleteActivity: (id: string) => void;
}
//WE are destructuring from the props coming from the Parent component. For TS, we specify the interface outside the brackets.
//width property in column is of 16 therfore they add up.
export default observer(function ActivityDashboard() {
  const { activityStore } = useStore();
  const { selectedActivity, editMode } = activityStore;

  return (
    <Grid>
      <Grid.Column width="10">
        <ActivityList />
      </Grid.Column>
      <Grid.Column width="6">
        {selectedActivity && !editMode && <ActivityDetail />}
        {editMode && <ActivityForm />}
      </Grid.Column>
    </Grid>
  );
});
