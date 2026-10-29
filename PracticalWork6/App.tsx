import React from 'react';
import { View, StyleSheet } from 'react-native';

import ArticleScreen from './screens/ArticleScreen';
import TasksScreen from './screens/TasksScreen';
import QuadrantScreen from './screens/QuadrantScreen';

// Измените число на 1, 2 или 3, чтобы открыть нужное упражнение.
const ACTIVE_SCREEN: number = 1;

export default function App() {
  let screen: React.ReactElement;

  switch (ACTIVE_SCREEN) {
    case 2:
      screen = <TasksScreen />;
      break;
    case 3:
      screen = <QuadrantScreen />;
      break;
    case 1:
    default:
      screen = <ArticleScreen />;
  }

  return <View style={styles.container}>{screen}</View>;
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#FFFFFF',
  },
});
