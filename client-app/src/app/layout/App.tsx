import { useEffect } from "react";
import { Container } from "semantic-ui-react";

import NavBar from "./NavBar";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";

import agent from "../api/agent";
import LoadingComponent from "./LoadingComponent";
import { useStore } from "../stores/store";
import { observer } from "mobx-react-lite";

//Going to setup an interface for App.tsx?

function App() {
  const { activityStore } = useStore();

  //Makes a call to API once without dependencies, which sets activities array
  useEffect(() => {
    agent.Activities.list().then(() => {
      activityStore.loadActivities();
    });
  }, [activityStore]);

  if (activityStore.loadingInitial) return <LoadingComponent content="Loading app" />;

  return (
    <>
      <NavBar />
      <Container style={{ marginTop: "7em" }}>
        <ActivityDashboard />
      </Container>
    </>
  );
}

//Import observer and wrap App as its parameter
export default observer(App);
