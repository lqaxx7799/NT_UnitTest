import { makeStyles, shorthands } from "@fluentui/react-components";

export const useGlobalClasses = makeStyles({
  pageHeader: {
    marginBottom: '24px',
  },
  pageActionGroup: {
    marginBottom: '12px',
  },
  formControlGroup: {
    display: 'flex',
    flexDirection: 'column',
    ...shorthands.gap('4px'),
  },
});
