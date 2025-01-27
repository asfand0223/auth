import React, { useEffect, useRef } from "react";
import styles from "@/styles/auth/register.module.scss";
import { useDispatch } from "react-redux";
import { setUsername, setPassword, setConfirmPassword } from "@/redux/register";
import { register, authorise } from "@/api/auth";
import {
  setError,
  setValidationErrors,
  setIsSubmittable,
} from "@/redux/register";
import { useSelector } from "react-redux";
import { RootState } from "@/redux/store";
import Error from "./error";
import ValidationErrors from "./validation_errors";
import { setSelf } from "@/redux/auth";

const Register = () => {
  const dispatch = useDispatch();
  const {
    error,
    validation_errors,
    username,
    password,
    confirm_password,
    is_submittable,
  } = useSelector((state: RootState) => state.register);
  const username_ref = useRef<HTMLInputElement>(null);
  const password_ref = useRef<HTMLInputElement>(null);
  const confirm_password_ref = useRef<HTMLInputElement>(null);

  useEffect(() => {
    dispatch(setError({ error: null }));
    dispatch(setValidationErrors({ validation_errors: null }));
  }, [dispatch]);

  useEffect(() => {
    dispatch(
      setIsSubmittable({
        is_submittable:
          username !== "" &&
          password !== "" &&
          password.length >= 8 &&
          confirm_password !== "" &&
          password === confirm_password,
      }),
    );
  }, [dispatch, username, password, confirm_password]);

  const handleUsernameInputChange = () => {
    dispatch(setUsername({ username: username_ref.current?.value ?? "" }));
  };

  const handlePasswordInputChange = () => {
    dispatch(setPassword({ password: password_ref.current?.value ?? "" }));
  };

  const handleConfirmPasswordInputChange = () => {
    dispatch(
      setConfirmPassword({
        confirm_password: confirm_password_ref.current?.value ?? "",
      }),
    );
  };

  const handleFormSubmit = async (e: React.FormEvent<HTMLElement>) => {
    try {
      e.preventDefault();
      e.stopPropagation();
      dispatch(setError({ error: null }));
      dispatch(setValidationErrors({ validation_errors: null }));
      if (
        !username_ref.current ||
        !password_ref.current ||
        !confirm_password_ref.current ||
        password_ref.current.value !== confirm_password_ref.current.value
      )
        return;
      const response = await register({
        username: username_ref.current.value,
        password: password_ref.current.value,
        confirm_password: confirm_password_ref.current.value,
      });
      if (response.status === 200) {
        const response = await authorise();
        dispatch(setSelf({ self: response }));
      } else if (response.error) {
        dispatch(setError({ error: { message: response.error } }));
      } else {
        dispatch(
          setValidationErrors({
            validation_errors: response.validation_errors,
          }),
        );
      }
    } catch (e) {
      console.error(e);
    }
  };

  return (
    <div className={styles.container}>
      {error && <Error error={error.message} />}
      <form className={styles.form} onSubmit={(e) => handleFormSubmit(e)}>
        <div className={styles.form_control}>
          <label>
            <b>Username*</b>
          </label>
          <br />
          <input
            type="text"
            placeholder={"Username"}
            ref={username_ref}
            onChange={handleUsernameInputChange}
          />
          <br />
          {validation_errors?.Username && (
            <ValidationErrors validation_errors={validation_errors.Username} />
          )}
        </div>
        <div className={styles.form_control}>
          <label>
            <b>Password* (must be at least 8 characters)</b>
          </label>
          <br />
          <input
            type="password"
            placeholder={"Password"}
            ref={password_ref}
            onChange={handlePasswordInputChange}
          />
          <br />
          {validation_errors?.Password && (
            <ValidationErrors validation_errors={validation_errors.Password} />
          )}
        </div>
        <div className={styles.form_control}>
          <label>
            <b>Confirm Password*</b>
          </label>
          <br />
          <input
            type="password"
            placeholder={"Confirm Password"}
            ref={confirm_password_ref}
            onChange={handleConfirmPasswordInputChange}
          />
          <br />
          {validation_errors?.ConfirmPassword && (
            <ValidationErrors
              validation_errors={validation_errors.ConfirmPassword}
            />
          )}
        </div>
        <div className={styles.form_control}>
          <button
            className={`${styles.register_button} ${is_submittable ? styles.register_button_active : ""}`}
            type="submit"
            disabled={!is_submittable}
          >
            Register
          </button>
        </div>
      </form>
    </div>
  );
};

export default Register;
