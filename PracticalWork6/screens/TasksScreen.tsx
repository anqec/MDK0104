import React from 'react';
import { Image, StyleSheet, Text, View } from 'react-native';

export default function TasksScreen() {
  return (
    <View style={styles.container}>
      <Image
        source={require('../assets/ic_task_completed.png')}
        style={styles.image}
        resizeMode="contain"
      />

      <Text style={styles.title}>Все задания выполнены</Text>
      <Text style={styles.subtitle}>Отличная работа!</Text>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
  },
  image: {
    width: 180,
    height: 180,
  },
  title: {
    fontWeight: 'bold',
    marginTop: 24,
    marginBottom: 8,
    textAlign: 'center',
  },
  subtitle: {
    fontSize: 16,
    textAlign: 'center',
  },
});
