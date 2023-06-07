import { ErrorMessage, Form, Formik } from "formik";
import MyTextInput from "../../app/common/form/MyTextInput";
import { Button, Header, Label } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { store } from "../../app/stores/store";
//This loginform will use our new store called userStore. First we have to get access to the store from strore.ts
//From MobX we utilize their special method called Observer. This is like hook that monitors changes with state.

const LoginForm = observer(() => {
  //We have to destructure our store from the actual store.
  const { userStore } = store;

  //About Formik
  //Inside the properties we set for the Formik component, we have a property onSubmit which handles a function.
  //This function interacts with the second parameter, isSubmitting
  //We utilize the MobX store userStore.login to store the values when the form is submitted
  //To start client-side errors, we place an object next to values and see we have helper methods to use.
  return (
    <Formik
      initialValues={{ email: "", password: "", error: null }}
      onSubmit={(values, { setErrors }) =>
        userStore.login(values).catch((error) => setErrors({ error: "Invalid email or password" }))
      }
    >
      {({ handleSubmit, isSubmitting, errors }) => (
        <Form className="ui form" onSubmit={handleSubmit} autoComplete="off">
          <Header as='h2' content='Login to Reactivities' color='teal' textAlign='center'/>
          <MyTextInput placeholder={"Email"} name={"email"} />
          <MyTextInput placeholder={"Password"} name={"password"} type={"password"} />
          <ErrorMessage
            name={"error"}
            render={() => (
              <Label style={{ marginBottom: 10 }} basic color="red" content={errors.error}/>
            )}
          />
          <Button loading={isSubmitting} positive content="Login" type="submit" fluid />
        </Form>
      )}
    </Formik>
  );
});

export default LoginForm;
