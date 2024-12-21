//> using scala 3.6.2
import scala.collection.mutable._
import scala.util.boundary, boundary.break

@main
def hello(): Unit =
  val lines = scala.io.Source.fromFile("input.txt").mkString.split("\n")
  val height = lines.length
  val width = lines(0).length
  var start = (0, 0)
  var end = (0, 0)
  for y <- 0 to height - 1 do
    for x <- 0 to width - 1 do
      if (lines(y)(x) == 'S')
        start = (x, y)
      if (lines(y)(x) == 'E')
        end = (x, y)
  //println(s"$start, $end")

  val paths = lines.map(_.map(c => if (c == '#') then -1 else 999999).toArray()).toArray()

  val queue = Queue[(Int, Int, Int)]()
  queue.enqueue((start(0), start(1), 0))

  val directions = Array((-1, 0), (1, 0), (0, -1), (0, 1))
  var checkBounds = (x: Int, y: Int) => x >= 0 && y >= 0 && x < width && y < height
  while !queue.isEmpty do
    var (x, y, steps) = queue.dequeue()
    if (checkBounds(x, y) && paths(y)(x) > steps) then
      paths(y)(x) = steps
      for (dx, dy) <- directions do
        queue.enqueue((x + dx, y + dy, steps + 1))

  var count = 0
  val diffMin = 100
  for y <- 0 to height - 1 do
    for x <- 0 to width - 1 do
      if (paths(y)(x) == -1) then
        val minVal = directions.foldLeft(999999)((s, direction) =>
          val xx = x + direction(0)
          val yy = y + direction(1)
          val value = if (checkBounds(xx, yy)) then paths(yy)(xx) else -1
          if (value == -1) then s else if (value < s) then value else s)
        val maxVal = directions.foldLeft(-1)((s, direction) =>
          val xx = x + direction(0)
          val yy = y + direction(1)
          val value = if (checkBounds(xx, yy)) then paths(yy)(xx) else -1
          if (value == -1) then s else if (value > s) then value else s)
          val diff = maxVal - minVal - 2
          if (diff >= diffMin) then
            count = count + 1
            //println(s"$x, $y, $diff, $minVal, $maxVal")

  println(count)
