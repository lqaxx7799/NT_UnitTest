import { Outlet } from "react-router-dom";
import Sidebar from "./Sidebar";
import { makeStyles, shorthands } from "@fluentui/react-components";

type MainLayoutProps = {
};

const useStyles = makeStyles({
  root: {
    height: '100vh',
    display: 'flex',
    flexDirection: 'row',
  },
  content: {
    ...shorthands.padding('24px'),
  },
});

const MainLayout = ({}: MainLayoutProps) => {
  const classes = useStyles();

  return (
    <div className={classes.root}>
      <Sidebar />
      <div className={classes.content}>
        <Outlet />
      </div>
    </div>
  );
}

export default MainLayout;
