import { useEffect } from "react";
import { Grid, GridColumn } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { observer } from "mobx-react-lite";
import { useParams } from "react-router-dom";
import ActivityDetailedHeader from "./ActivityDetailedHeader";
import ActivityDetailedInfo from "./ActivityDetailedInfo";
import ActivityDetailedChat from "./ActivityDetailedChat";
import ActivityDetailedSideBar from "./ActivityDetailedSideBar";

export default observer(function ActivityDetail() {
  const { activityStore } = useStore();
  const { selectedActivity: activity, loadActivity, loadingInitial } = activityStore;
  const { id } = useParams();

  useEffect(() => {
    if (id) loadActivity(id);
  }, [id, loadActivity]);

  if (loadingInitial || !activity) return <LoadingComponent />;
  return <Grid>
    <Grid.Column width={'10'}>
      <ActivityDetailedHeader activity={activity}/>
      <ActivityDetailedInfo activity={activity}/>
      <ActivityDetailedChat />
    </Grid.Column>
    <GridColumn width={'6'}>
      <ActivityDetailedSideBar activity={activity}/>
    </GridColumn>
  </Grid>;
});
