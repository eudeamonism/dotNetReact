import { Button, Header, Segment } from "semantic-ui-react";
import { useState } from "react";
import { useStore } from "../../../app/stores/store";
import { v4 as uuid } from "uuid";
import { observer } from "mobx-react-lite";
import { Link, useNavigate, useParams } from "react-router-dom";
import { useEffect } from "react";
import { ActivityFormValues } from "../../../app/models/activity";
import LoadingComponent from "../../../app/layout/LoadingComponent";

//Formik is a wrapper that takes a parameter called initalValues which is the property it takes.
import { Formik, Form } from "formik";

//Yup with Formik
import * as Yup from "yup";
import MyTextInput from "../../../app/common/form/MyTextInput";
import MyTextAreaInput from "../../../app/common/form/MyTextArea";
import MySelectInputs from "../../../app/common/form/MySelectInputs";
import { categoryOptions } from "../../../app/common/options/categoryOptions";
import MyDateInput from "../../../app/common/form/MyDateInput";

//NOTICE we added MobX 'observer'. This is like a state switch that tells the store that if an update were to change
//then it shouldupdate the UI.
export default observer(function ActivityForm() {
  const { activityStore } = useStore();
  const { loadActivity, loadingInitial, createActivity, updateActivity } = activityStore;
  const { id } = useParams();
  const navigate = useNavigate();

  //This is our useState hook setting the initial activities where the default values are blank.
  const [activity, setActivity] = useState<ActivityFormValues>(new ActivityFormValues);

  //Yup Object which Formik has a validationSchema property where we pass this similarly named Yup object to it.
  //We then pass a Formik FormField wrapper for each input element, aka field so errors can be read clientside.
  const validationSchema = Yup.object({
    title: Yup.string().required("The activity title is required"),
    description: Yup.string().required("The description is required"),
    category: Yup.string().required(),
    city: Yup.string().required(),
    venue: Yup.string().required(),
    date: Yup.string().required('The date is required'),
  });

  //This hook loads data upon initialization and change and it pulls from the MobX store.
  useEffect(() => {
    if (id) loadActivity(id).then((activity) => setActivity(new ActivityFormValues(activity)))
  }, [id, loadActivity]);

  //handleSubmit
  function handleFormSubmit(activity: ActivityFormValues) {
    if (!activity.id) {
      let newActivity = {
        ...activity,
        id: uuid(),
      };
      createActivity(newActivity).then(() => navigate(`/activities/${newActivity.id}`));
    } else {
      updateActivity(activity).then(() => navigate(`/activities/${activity.id}`));
    }
  }

  //This condition returns a loading screen when data hasn't been loaded.
  if (loadingInitial) return <LoadingComponent content="Loading activity..." />;
  //Formik: use their handleSubmit only. From Library, Field takes care of the rest.

  return (
    <Segment clearing>
      <Header content="Activity Details" sub color="teal" />
      <Formik
        validationSchema={validationSchema}
        enableReinitialize
        initialValues={activity}
        onSubmit={(values) => handleFormSubmit(values)}
      >
        {({ handleSubmit, isValid, isSubmitting, dirty }) => (
          <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
            <MyTextInput name="title" placeholder="Title" />
            <MyTextAreaInput name="description" placeholder="Description" rows={3} />
            <MySelectInputs name="category" placeholder="Category" options={categoryOptions} />
            <MyDateInput
              name="date"
              placeholderText="Date"
              showTimeSelect
              timeCaption="time"
              dateFormat="MMMM d, yyyy h:mm aa"
            />
            <Header content="Location Details" sub color="teal" />
            <MyTextInput name="city" placeholder="City" />
            <MyTextInput name="venue" placeholder="Venue" />
            <Button disabled={isSubmitting || !dirty || !isValid} loading={isSubmitting} floated="right" positive type="submit" content="Submit" />
            <Button floated="right" type="button" content="Cancel" as={Link} to={"/activities"} />
          </Form>
        )}
      </Formik>
    </Segment>
  );
});
