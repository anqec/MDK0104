import React from 'react';
import { StyleSheet, Text, View } from 'react-native';

type QuadrantProps = {
  title: string;
  description: string;
  backgroundColor: string;
};

function Quadrant({ title, description, backgroundColor }: QuadrantProps) {
  return (
    <View style={[styles.quadrant, { backgroundColor }]}>
      <Text style={styles.title}>{title}</Text>
      <Text style={styles.description}>{description}</Text>
    </View>
  );
}

export default function QuadrantScreen() {
  return (
    <View style={styles.container}>
      <View style={styles.row}>
        <Quadrant
          title="Компонент Text"
          description="Отображает текст и поддерживает базовые стили шрифта."
          backgroundColor="#EADDFF"
        />
        <Quadrant
          title="Компонент Image"
          description="Отображает изображение из файла или из сети."
          backgroundColor="#D0BCFF"
        />
      </View>

      <View style={styles.row}>
        <Quadrant
          title="Компонент Row (ряд)"
          description="Аналог строки: компоненты View с flexDirection 'row' располагают элементы по горизонтали."
          backgroundColor="#B69DF8"
        />
        <Quadrant
          title="Компонент Column (колонка)"
          description="Компоненты View с flexDirection 'column' располагают элементы по вертикали."
          backgroundColor="#F6EDFF"
        />
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  row: {
    flex: 1,
    flexDirection: 'row',
  },
  quadrant: {
    flex: 1,
    padding: 16,
    justifyContent: 'center',
    alignItems: 'center',
  },
  title: {
    fontSize: 14,
    fontWeight: '600',
    marginBottom: 16,
    textAlign: 'center',
  },
  description: {
    fontSize: 14,
    textAlign: 'center',
  },
});
