import { useField } from "formik";
import { Form, Label } from "semantic-ui-react";

interface Props {
  placeholder: string;
  name: string;
  rows: number;
  label?: string;
}

export default function MyTextAreaInput(props: Props) {
  //useField(<name will retrieve the elements in with given name>)
  //!! converts such to a boolean, in our case could return string or null.
  const [field, meta] = useField(props.name);
  return (
    <Form.Field error={meta.touched && !!meta.error}>
      <label>{props.label}</label>
      <textarea {...field} {...props} />
      {meta.touched && meta.error ? <Label basic color="red" content={meta.error} /> : null}
    </Form.Field>
  );
}
