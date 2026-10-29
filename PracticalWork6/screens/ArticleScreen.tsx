import React from 'react';
import { Image, ScrollView, StyleSheet, Text } from 'react-native';

export default function ArticleScreen() {
  return (
    <ScrollView style={styles.container}>
      <Image
        source={require('../assets/bg_article_background.png')}
        style={styles.image}
        resizeMode="cover"
      />

      <Text style={styles.title}>Учебник по React Native</Text>

      <Text style={styles.paragraphTwo}>
        React Native — это фреймворк для создания нативных мобильных
        приложений с помощью TypeScript. Он позволяет писать код один раз и
        запускать его на Android и iOS, используя нативные компоненты.
      </Text>

      <Text style={styles.paragraphThree}>
        В этом уроке вы создадите простой интерфейс с помощью декларативных
        компонентов. Вы описываете, какие элементы нужны, а React Native сам
        отрисовывает их на экране.
      </Text>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#FFFFFF',
  },
  image: {
    width: '100%',
    height: 220,
  },
  title: {
    fontSize: 24,
    padding: 16,
  },
  paragraphTwo: {
    fontSize: 14,
    paddingHorizontal: 16,
    textAlign: 'justify',
  },
  paragraphThree: {
    fontSize: 14,
    padding: 16,
    textAlign: 'justify',
  },
});
